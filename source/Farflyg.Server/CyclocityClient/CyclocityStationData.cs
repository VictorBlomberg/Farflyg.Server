using System;

namespace Farflyg.Server.CyclocityClient
{
    /// <Immutabler />
    public sealed partial class CyclocityStationData
    {
        public int Number { get; }

        public string Name { get; }

        public string Address { get; }

        public double PositionLatitude { get; }

        public double PositoinLongitude { get; }

        public bool Banking { get; }

        public bool Bonus { get; }

        public string Status { get; }

        public string ContractName { get; }

        public int BikeStands { get; }

        public int AvailableBikeStands { get; }

        public int AvailableBikes { get; }

        public DateTimeOffset DataUpdated { get; }
    }
}
