namespace Farflyg.Server.Web
{
    using Owin;

    public abstract class SubStartupBase : ISubStartup
    {
        protected SubStartupBase(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public abstract void Configuration(IAppBuilder app);
    }
}
