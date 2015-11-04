namespace Farflyg.Server
{
    partial class GeoCoordinate
    {
        private GeoCoordinate(double @latitude, double @longitude)
        {
            Latitude = @latitude;
            Longitude = @longitude;
        }

        public static GeoCoordinate Create(double @latitude, double @longitude)
        {
            var _instance = new GeoCoordinate(@latitude, @longitude);
            return _instance;
        }

        public GeoCoordinate WithLatitude(double @latitude)
        {
            var _instance = new GeoCoordinate(@latitude, Longitude);
            return _instance;
        }

        public GeoCoordinate WithLongitude(double @longitude)
        {
            var _instance = new GeoCoordinate(Latitude, @longitude);
            return _instance;
        }
    }
}