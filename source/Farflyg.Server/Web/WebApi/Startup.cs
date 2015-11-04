namespace Farflyg.Server.Web.WebApi
{
    using System.Web.Http;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    public sealed class Startup : SubStartupBase
    {
        private readonly IKernel _Kernel;

        public Startup(IKernel kernel)
            : base("/webapi")
        {
            _Kernel = kernel;
        }

        public override void Configuration(IAppBuilder app)
        {
            var _config = new HttpConfiguration();

            _config.Formatters.Remove(_config.Formatters.XmlFormatter);

            _config.MapHttpAttributeRoutes();
            _config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            app
                .UseNinjectMiddleware(() => _Kernel)
                .UseNinjectWebApi(_config);
        }
    }
}
