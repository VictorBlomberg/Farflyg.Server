namespace Farflyg.Server
{
    using System;
    using System.Runtime.Caching;

    using Core;

    using CyclocityClient;

    using JetBrains.Annotations;

    using Ninject;

    using Web;

    public static class NinjectConfiguration
    {
        [PublicAPI]
        public static IKernel CreateDefaultKernel()
        {
            var _kernel = new StandardKernel();
            
            _kernel
                .Bind<CyclocityCredentials>()
                .ToConstant(CyclocityCredentials.GetFromConfiguration());

            _kernel
                .Bind<ICyclocityRepository>()
                .To<CyclocityRepository>()
                .InSingletonScope();

            _kernel
                .Bind<ObjectCache>()
                .ToConstant(MemoryCache.Default);

            _kernel
                .Bind<CacheItemPolicy>()
                .ToConstant(new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30), });

            _kernel
                .Bind<IMainRepository>()
                .To<MainRepository>();
            
            _kernel.Bind<ISubStartup>().To<Web.ClientAccessPolicy.Startup>();
            _kernel.Bind<ISubStartup>().To<Web.WebApi.Startup>();

            return _kernel;
        }
    }
}
