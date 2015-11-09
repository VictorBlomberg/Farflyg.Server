namespace Farflyg.Server.CyclocityClient
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    using Flurl;
    using Flurl.Http;

    public sealed class CyclocityRepository : ICyclocityRepository
    {
        private readonly string _BaseUrl;

        public CyclocityRepository(CyclocityCredentials credentials)
        {
            _BaseUrl = credentials.BaseUrl
                .SetQueryParam("apiKey", credentials.ApiKey)
                .SetQueryParam("contract", credentials.Contract);
        }

        public async Task<ImmutableList<CyclocityStationData>> GetStationsDataAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var _dataUpdated = DateTimeOffset.Now;
            var _items = await _BaseUrl
                .AppendPathSegment("stations")
                .GetJsonListAsync(cancellationToken);

            return _items
                .ToImmutableList()
                .ConvertAll<CyclocityStationData>(_stationData => _ToStation(_dataUpdated, _stationData));
        }

        private CyclocityStationData _ToStation(DateTimeOffset dataUpdated, dynamic stationData)
        {
            return CyclocityStationData.Create(
                number: (int)stationData.number, 
                name: (string)stationData.name,
                address: (string)stationData.address,
                positionLatitude: (double)stationData.position.lat,
                positoinLongitude: (double)stationData.position.lng,
                banking: (bool)stationData.banking, 
                bonus: (bool)stationData.bonus, 
                status: (string)stationData.status, 
                contractName: (string)stationData.contract_name, 
                bikeStands: (int)stationData.bike_stands, 
                availableBikeStands: (int)stationData.available_bike_stands, 
                availableBikes: (int)stationData.available_bikes,
                dataUpdated: dataUpdated);
        }
    }
}
