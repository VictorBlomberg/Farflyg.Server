namespace Farflyg.Server
{
    partial class StationStatus
    {
        private StationStatus(Station @station, int @availableBikes, int @availableStands)
        {
            Station = @station;
            AvailableBikes = @availableBikes;
            AvailableStands = @availableStands;
        }

        public static StationStatus Create(Station @station, int @availableBikes, int @availableStands)
        {
            var _instance = new StationStatus(@station, @availableBikes, @availableStands);
            return _instance;
        }

        public StationStatus WithStation(Station @station)
        {
            var _instance = new StationStatus(@station, AvailableBikes, AvailableStands);
            return _instance;
        }

        public StationStatus WithAvailableBikes(int @availableBikes)
        {
            var _instance = new StationStatus(Station, @availableBikes, AvailableStands);
            return _instance;
        }

        public StationStatus WithAvailableStands(int @availableStands)
        {
            var _instance = new StationStatus(Station, AvailableBikes, @availableStands);
            return _instance;
        }
    }
}