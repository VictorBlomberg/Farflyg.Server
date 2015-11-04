namespace Farflyg.Server.Core
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    using CyclocityClient;

    public sealed class MainRepository : IMainRepository
    {
        private readonly ICyclocityRepository _CyclocityRepository;

        public MainRepository(ICyclocityRepository cyclocityRepository)
        {
            _CyclocityRepository = cyclocityRepository;
        }

        public async Task<ImmutableList<Station>> GetStationsAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var _stationsData = await _CyclocityRepository.GetStationsDataAsync(cancellationToken);

            return _stationsData.ConvertAll(_CreateStation);
        }

        public async Task<Station> GetStationAsync(int stationId, CancellationToken cancellationToken = new CancellationToken())
        {
            var _stationsData = await _CyclocityRepository.GetStationsDataAsync(cancellationToken);
            var _stationData = _stationsData.Find(_item => _item.Number == stationId);
            return _stationData == null ? null : _CreateStation(_stationData);
        }

        public async Task<ImmutableList<StationStatus>> GetStationStatusesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var _stationsData = await _CyclocityRepository.GetStationsDataAsync(cancellationToken);

            return _stationsData.ConvertAll(_CreateStationStatus);
        }

        public async Task<StationStatus> GetStationStatusAsync(int stationId, CancellationToken cancellationToken = new CancellationToken())
        {
            var _stationsData = await _CyclocityRepository.GetStationsDataAsync(cancellationToken);
            var _stationData = _stationsData.Find(_item => _item.Number == stationId);
            return _stationData == null ? null : _CreateStationStatus(_stationData);
        }

        private static Station _CreateStation(CyclocityStationData stationData)
        {
            var _name = string.Equals(stationData.Name, stationData.Address, StringComparison.OrdinalIgnoreCase)
                ? stationData.Address
                : _FixStationDisplayNameCapitalization(stationData.Name);

            return Station.Create(
                id: stationData.Number,
                displayName: _name,
                location: GeoCoordinate.Create(latitude: stationData.PositionLatitude, longitude: stationData.PositoinLongitude),
                stands: stationData.BikeStands);
        }

        private static StationStatus _CreateStationStatus(CyclocityStationData stationData)
        {
            return StationStatus.Create(
                station: _CreateStation(stationData),
                availableBikes: stationData.AvailableBikes,
                availableStands: stationData.AvailableBikeStands);
        }

        private static string _FixStationDisplayNameCapitalization(string name)
        {
            var _nameChars = name.ToCharArray();
            for (var _i = 1; _i < _nameChars.Length; _i++)
            {
                if (char.IsLetter(_nameChars[_i - 1]))
                {
                    _nameChars[_i] = char.ToLower(_nameChars[_i]);
                }
            }

            return new string(_nameChars);
        }
    }
}
