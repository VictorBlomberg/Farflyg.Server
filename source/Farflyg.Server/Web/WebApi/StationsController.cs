namespace Farflyg.Server.Web.WebApi
{
    using System.Threading.Tasks;
    using System.Web.Http;

    public sealed class StationsController : ApiController
    {
        private readonly IMainRepository _Repository;

        public StationsController(IMainRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync()
        {
            return Ok((await _Repository.GetStationsAsync()).ConvertAll(_Export));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var _station = await _Repository.GetStationAsync(id);
            return _station == null ? NotFound() : (IHttpActionResult)Ok(_Export(_station));
        }

        private static object _Export(Station station)
        {
            return new
                {
                    id = station.Id,
                    displayName = station.DisplayName,
                    lat = station.Location.Latitude,
                    lon = station.Location.Longitude,
                    stands = station.Stands,
                };
        }
    }
}
