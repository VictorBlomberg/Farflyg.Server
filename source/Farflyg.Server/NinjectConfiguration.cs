using System.Configuration;

using Nerven.Assertion;

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
        public static string GenerateFakeDataConfigurationKey => typeof (NinjectConfiguration).FullName + ".GenerateFakeData";

        [PublicAPI]
        public static IKernel CreateDefaultKernel()
        {
            var _generateFakeData = _ReadBoolFromConfiguration(GenerateFakeDataConfigurationKey, false);
            return CreateDefaultKernel(_generateFakeData);
        }

        [PublicAPI]
        public static IKernel CreateDefaultKernel(bool generateFakeData)
        {
            var _kernel = new StandardKernel();

            _kernel
                .Bind<CyclocityCredentials>()
                .ToMethod(_context => CyclocityCredentials.GetFromConfiguration())
                .InSingletonScope();

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

            if (generateFakeData)
            {
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
                    .Bind<ICyclocityRepository>()
                    .To<FakeStatusCyclocityRepository>()
                    .InSingletonScope();

                _kernel
                    .Bind<ICyclocityRepository>()
                    .To<CyclocityRepository>()
                    .WhenInjectedInto<FakeStatusCyclocityRepository>();
            }
            else
            {
                _kernel
                    .Bind<ICyclocityRepository>()
                    .To<CachingCyclocityRepository>()
                    .InSingletonScope();

                _kernel
                    .Bind<ICyclocityRepository>()
                    .To<CyclocityRepository>()
                    .WhenInjectedInto<CachingCyclocityRepository>();

                _kernel
                    .Bind<CacheItemPolicy>()
                    .ToMethod(_context => new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30), })
                    .WhenInjectedInto<CachingCyclocityRepository>();

                _kernel
                    .Bind<ObjectCache>()
                    .ToConstant(MemoryCache.Default)
                    .WhenInjectedInto<CachingCyclocityRepository>();
            }

            _kernel
                .Bind<IMainRepository>()
                .To<MainRepository>();

            _kernel.Bind<ISubStartup>().To<Web.ClientAccessPolicy.Startup>();
            _kernel.Bind<ISubStartup>().To<Web.WebApi.Startup>();
            _kernel.Bind<ISubStartup>().To<Web.Readme.Startup>();

            return _kernel;
        }

        private static bool _ReadBoolFromConfiguration(string configurationKey, bool defaultValue)
        {
            var _value = ConfigurationManager.AppSettings[configurationKey];

            if (string.Equals(_value, "true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(_value, "false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            Must.Assert(string.IsNullOrEmpty(_value), $"Value of configuration setting {configurationKey} is not a valid boolean. Must be empty, 'true' or 'false'.");

            return defaultValue;
        }
    }
}
