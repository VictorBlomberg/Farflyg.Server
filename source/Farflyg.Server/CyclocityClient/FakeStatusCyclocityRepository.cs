namespace Farflyg.Server.CyclocityClient
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    using Nerven.Taskuler;

    using Nito.AsyncEx;

    public sealed class FakeStatusCyclocityRepository : ICyclocityRepository
    {
        private readonly ICyclocityRepository _InnerRepository;
        private readonly IFakeGenerator _FakeStatusGenerator;
        private readonly AsyncReaderWriterLock _StationsListLock;

        private ImmutableList<CyclocityStationData> _Stations;

        public FakeStatusCyclocityRepository(
            ICyclocityRepository innerRepository,
            IFakeGenerator fakeStatusGenerator,
            ITaskulerScheduleHandle updateFakeDataScheduleHandle)
        {
            _InnerRepository = innerRepository;
            _FakeStatusGenerator = fakeStatusGenerator;
            _StationsListLock = new AsyncReaderWriterLock();

            updateFakeDataScheduleHandle.AddTask(async () => await _UpdateFakeDataAsync());
        }

        public async Task<ImmutableList<CyclocityStationData>> GetStationsDataAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (await _StationsListLock.ReaderLockAsync())
            {
                if (_Stations != null)
                {
                    return _Stations;
                }
            }

            using (var _lock = await _StationsListLock.UpgradeableReaderLockAsync())
            {
                var _stations = ImmutableList.CreateRange(await _InnerRepository.GetStationsDataAsync(cancellationToken));
                using (await _lock.UpgradeAsync())
                {
                    _Stations = _stations;
                }

                return _stations;
            }
        }

        private async Task _UpdateFakeDataAsync()
        {
            using (var _lock = await _StationsListLock.UpgradeableReaderLockAsync())
            {
                if (_Stations != null)
                {
                    var _stations = _Stations.ConvertAll(_station => _FakeStatusGenerator.Generate(_station));
                    using (await _lock.UpgradeAsync())
                    {
                        _Stations = _stations;
                    }
                }
            }
        }

        public interface IFakeGenerator
        {
            CyclocityStationData Generate(CyclocityStationData station);
        }

        public sealed class DefaultFakeGenerator : IFakeGenerator
        {
            private readonly Random _Random;
            private readonly int _TimeFactor;

            private int _Trend;

            public DefaultFakeGenerator(Random random, TimeSpan updateInterval)
            {
                _Random = random;
                _TimeFactor = (int)Math.Floor(60D / updateInterval.TotalSeconds);
                _Trend = _Random.Next(2) == 0 ? 1 : -1;
            }

            public CyclocityStationData Generate(CyclocityStationData station)
            {
                int _availableBikesChange;
                if (_Random.Next(_TimeFactor) == 0)
                {
                    var _rnd = _Random.Next(3);
                    if (_rnd == 1 || (_rnd == 0 && _Trend == 1))
                    {
                        _availableBikesChange = -_Random.Next(0, Math.Min(station.AvailableBikes, 5));
                        _Trend = 1;
                    }
                    else
                    {
                        _availableBikesChange = _Random.Next(0, Math.Min(station.BikeStands - station.AvailableBikes, 5));
                        _Trend = -1;
                    }
                }
                else
                {
                    _availableBikesChange = 0;
                }

                return station
                    .WithAvailableBikes(station.AvailableBikes + _availableBikesChange)
                    .WithAvailableBikeStands(station.AvailableBikeStands + (_availableBikesChange * -1));
            }
        }
    }
}
