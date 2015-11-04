namespace Farflyg.Server
{
    /// <Immutabler />
    public sealed partial class StationStatus
    {
        public Station Station { get; }

        public int AvailableBikes { get; }

        public int AvailableStands { get; }
    }
}
