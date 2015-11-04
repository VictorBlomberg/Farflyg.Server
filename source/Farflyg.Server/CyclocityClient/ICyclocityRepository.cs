namespace Farflyg.Server.CyclocityClient
{
    using System.Collections.Immutable;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICyclocityRepository
    {
        Task<ImmutableList<CyclocityStationData>> GetStationsDataAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
