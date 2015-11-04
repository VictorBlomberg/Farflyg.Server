namespace Farflyg.Server.Web
{
    using System;

    using JetBrains.Annotations;

    using Ninject;

    using Owin;

    [PublicAPI]
    public sealed class Startup : IDisposable
    {
        private readonly IKernel _Kernel;
        private IKernel _DisposableKernel;

        public Startup(IKernel kernel = null)
        {
            _Kernel = kernel ?? (_DisposableKernel = NinjectConfiguration.CreateDefaultKernel());
        }

        public void Configuration(IAppBuilder app)
        {
            app.Use((_http, _next) =>
                {
                    if (_http.Request.Headers.ContainsKey("Origin"))
                    {
                        _http.Response.Headers.Set("Access-Control-Allow-Origin", "*");
                    }

                    return _next();
                });

            foreach (var _subStartup in _Kernel.GetAll<ISubStartup>())
            {
                app.Map(_subStartup.Path, _subApp => _subStartup.Configuration(_subApp));
            }
        }

        public void Dispose()
        {
            if (_DisposableKernel != null)
            {
                _DisposableKernel.Dispose();
                _DisposableKernel = null;
            }
        }
    }
}
