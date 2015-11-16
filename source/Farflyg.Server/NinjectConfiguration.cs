namespace Farflyg.Server
{
    using System;
    using System.Runtime.Caching;

    using Core;

    using CyclocityClient;

    using JetBrains.Annotations;

    using Nerven.Taskuler;
    using Nerven.Taskuler.Core;

    using Ninject;

    using Web;

    public static class NinjectConfiguration
    {
        [PublicAPI]
        public static IKernel CreateDefaultKernel()
        {
            var _kernel = new StandardKernel();

            _kernel
                .Bind<Random>()
                .ToSelf()
                .InSingletonScope();

            _kernel
                .Bind<ITaskulerWorker>()
                .ToMethod(_context => TaskulerWorker.Create())
                .InSingletonScope()
                .OnActivation((_context, _worker) =>
                    {
                        _worker.StartAsync();
                    });

            var _fakeUpdateSchedule = TimeSpan.FromSeconds(10);
            _kernel
                .Bind<ITaskulerScheduleHandle>()
                .ToMethod(_context => _context.Kernel.Get<ITaskulerWorker>().AddIntervalSchedule(_fakeUpdateSchedule))
                .WhenInjectedInto<FakeStatusCyclocityRepository>()
                .InSingletonScope();

            _kernel
                .Bind<TimeSpan>()
                .ToConstant(_fakeUpdateSchedule)
                .WhenInjectedInto<FakeStatusCyclocityRepository.DefaultFakeGenerator>();

            _kernel
                .Bind<FakeStatusCyclocityRepository.IFakeGenerator>()
                .To<FakeStatusCyclocityRepository.DefaultFakeGenerator>()
                .InSingletonScope();

            _kernel
                .Bind<CyclocityCredentials>()
                .ToMethod(_context => CyclocityCredentials.GetFromConfiguration())
                .InSingletonScope();

            _kernel
                .Bind<ICyclocityRepository>()
                .To<CyclocityRepository>()
                .WhenInjectedInto<CachingCyclocityRepository>();

            _kernel
                .Bind<ICyclocityRepository>()
                .To<CachingCyclocityRepository>()
                .WhenInjectedInto<FakeStatusCyclocityRepository>()
                .InSingletonScope();

            _kernel
                .Bind<ICyclocityRepository>()
                .To<FakeStatusCyclocityRepository>()
                .InSingletonScope();

            _kernel
                .Bind<ObjectCache>()
                .ToConstant(MemoryCache.Default);

            _kernel
                .Bind<CacheItemPolicy>()
                .ToMethod(_context => new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30), });

            _kernel
                .Bind<IMainRepository>()
                .To<MainRepository>();
            
            _kernel.Bind<ISubStartup>().To<Web.ClientAccessPolicy.Startup>();
            _kernel.Bind<ISubStartup>().To<Web.WebApi.Startup>();

            return _kernel;
        }
    }
}
