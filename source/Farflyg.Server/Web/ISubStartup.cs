namespace Farflyg.Server.Web
{
    using Owin;

    public interface ISubStartup
    {
        string Path { get; }

        void Configuration(IAppBuilder app);
    }
}
