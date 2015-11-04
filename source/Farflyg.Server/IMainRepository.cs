namespace Farflyg.Server
{
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMainRepository
    {
        Task<ImmutableList<Station>> GetStationsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<Station> GetStationAsync(int stationId, CancellationToken cancellationToken = default(CancellationToken));

        Task<ImmutableList<StationStatus>> GetStationStatusesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<StationStatus> GetStationStatusAsync(int stationId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
