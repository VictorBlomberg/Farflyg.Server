namespace Farflyg.Server.CyclocityClient
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Nerven.Taskuler;

    using Nito.AsyncEx;

    public sealed class FakeStatusCyclocityRepository : ICyclocityRepository
    {
        private readonly ICyclocityRepository _InnerRepository;
        private readonly IFakeStatusGenerator _FakeStatusGenerator;
        private readonly AsyncReaderWriterLock _StationsListLock;

        private ImmutableList<_StationRecord> _Stations;

        public FakeStatusCyclocityRepository(
            ICyclocityRepository innerRepository,
            IFakeStatusGenerator fakeStatusGenerator,
            ITaskulerScheduleHandle updateFakeDataScheduleHandle)
        {
            _InnerRepository = innerRepository;
            _FakeStatusGenerator = fakeStatusGenerator;
            _StationsListLock = new AsyncReaderWriterLock();

            updateFakeDataScheduleHandle.AddTask(() =>
                {
                    _UpdateFakeData();
                    return Task.CompletedTask;
                });
        }

        public async Task<ImmutableList<CyclocityStationData>> GetStationsDataAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ImmutableList<CyclocityStationData> _stations = null;

            using (await _StationsListLock.ReaderLockAsync())
            {
                if (_Stations != null)
                {
                    _stations = _Stations.ConvertAll(_station => _station.Data);
                }
            }
            
            if (_stations == null)
            {
                using (var _lock = await _StationsListLock.UpgradeableReaderLockAsync())
                {
                    var _innerData = await _InnerRepository.GetStationsDataAsync(cancellationToken);

                    using (await _lock.UpgradeAsync())
                    {
                        _Stations = ImmutableList.CreateRange(_innerData.Select(_stationData => new _StationRecord(_stationData)));
                    }

                    _stations = _Stations.ConvertAll(_station => _station.Data);
                }
            }

            return _stations;
        }

        private void _UpdateFakeData()
        {
            using (_StationsListLock.WriterLock())
            {
                if (_Stations != null)
                {
                    foreach (var _station in _Stations)
                    {
                        _station.Data = _FakeStatusGenerator.Generate(_station.Data);
                    }
                }
            }
        }

        public interface IFakeStatusGenerator
        {
            CyclocityStationData Generate(CyclocityStationData station);
        }

        public sealed class DefaultFakeStatusGenerator : IFakeStatusGenerator
        {
            private readonly Random _Random;
            private readonly int _TimeFactor;

            private int _Trend;

            public DefaultFakeStatusGenerator(Random random, TimeSpan updateInterval)
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
                    .WithAvailableBikeStands(station.AvailableBikeStands + (_availableBikesChange*-1));
            }
        }

        private sealed class _StationRecord
        {
            public _StationRecord(CyclocityStationData data)
            {
                Data = data;
            }

            public CyclocityStationData Data { get; set; }
        }
    }
}
