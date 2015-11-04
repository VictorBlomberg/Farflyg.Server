namespace Farflyg.Server
{
    partial class Station
    {
        private Station(int @id, string @displayName, GeoCoordinate @location, int @stands)
        {
            Id = @id;
            DisplayName = @displayName;
            Location = @location;
            Stands = @stands;
        }

        public static Station Create(int @id, string @displayName, GeoCoordinate @location, int @stands)
        {
            var _instance = new Station(@id, @displayName, @location, @stands);
            return _instance;
        }

        public Station WithId(int @id)
        {
            var _instance = new Station(@id, DisplayName, Location, Stands);
            return _instance;
        }

        public Station WithDisplayName(string @displayName)
        {
            var _instance = new Station(Id, @displayName, Location, Stands);
            return _instance;
        }

        public Station WithLocation(GeoCoordinate @location)
        {
            var _instance = new Station(Id, DisplayName, @location, Stands);
            return _instance;
        }

        public Station WithStands(int @stands)
        {
            var _instance = new Station(Id, DisplayName, Location, @stands);
            return _instance;
        }
    }
}