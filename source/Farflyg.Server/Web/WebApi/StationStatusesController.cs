namespace Farflyg.Server.Web.WebApi
{
    using System.Threading.Tasks;
    using System.Web.Http;

    public sealed class StationStatusesController : ApiController
    {
        private readonly IMainRepository _Repository;

        public StationStatusesController(IMainRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok((await _Repository.GetStationStatusesAsync()).ConvertAll(_stationStatus => new
                {
                    station = new
                        {
                            id = _stationStatus.Station.Id,
                        },
                    availableStands = _stationStatus.AvailableStands,
                    availableBikes = _stationStatus.AvailableBikes,
                }));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var _stationStatus = await _Repository.GetStationStatusAsync(id);
            return _stationStatus == null ? NotFound() : (IHttpActionResult)Ok(_Export(_stationStatus));
        }
        
        private static object _Export(StationStatus stationStatus)
        {
            return new
                {
                    station = new
                    {
                        id = stationStatus.Station.Id,
                    },
                    availableStands = stationStatus.AvailableStands,
                    availableBikes = stationStatus.AvailableBikes,
                };
        }
    }
}
