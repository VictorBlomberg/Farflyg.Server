namespace Farflyg.Server.Runners.Selfhost
{
    using System;

    using Microsoft.Owin.Hosting;

    using Web;

    public static class Program
    {
        public const string DefaultBaseUrl = "http://localhost:32093";

        public static void Main(string baseUrl)
        {
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine(baseUrl);
                Console.ReadLine();
            }
        }

        public static void Main()
        {
            Main(DefaultBaseUrl);
        }
    }
}
