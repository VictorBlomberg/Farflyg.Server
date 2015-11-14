namespace Farflyg.Server.Web.ClientAccessPolicy
{
    using Owin;

    /// <summary>
    /// Used to let Silverlight in the browser access the server.
    /// </summary>
    public sealed class Startup : SubStartupBase
    {
        public Startup()
            : base("/clientaccesspolicy.xml")
        {
        }

        public override void Configuration(IAppBuilder app)
        {
            app.Map(
                string.Empty,
                _rootApp => _rootApp.Run(async _http =>
                    {
                        _http.Response.StatusCode = 200;
                        _http.Response.ContentType = "application/xml; charset=utf-8";

                        await _http.Response.WriteAsync(@"<?xml version=""1.0"" encoding=""utf-8""?>
<access-policy>
  <cross-domain-access>
    <policy>
      <allow-from http-request-headers=""*"" http-methods=""*"">
        <domain uri=""*""/>
      </allow-from>
      <grant-to>
        <resource path=""/"" include-subpaths=""true""/>
      </grant-to>
    </policy>
  </cross-domain-access>
</access-policy>
");
                    }));
        }
    }
}
