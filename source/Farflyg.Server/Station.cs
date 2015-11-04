namespace Farflyg.Server
{
    /// <Immutabler />
    public sealed partial class Station
    {
        public int Id { get; }

        public string DisplayName { get; }

        public GeoCoordinate Location { get; }

        public int Stands { get; }
    }
}
