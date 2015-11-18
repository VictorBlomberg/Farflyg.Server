using Owin;

namespace Farflyg.Server.Web.Readme
{
    public sealed class Startup : SubStartupBase
    {
        public Startup()
            : base(string.Empty)
        {
        }

        public override void Configuration(IAppBuilder app)
        {
            app.MapWhen(
                _http => _http.Request.Path.Value == "/",
                _rootApp => _rootApp.Run(async _http =>
                    {
                        _http.Response.StatusCode = 200;
                        _http.Response.ContentType = "text/html; charset=utf-8";

                        await _http.Response.WriteAsync(@"<!doctype html>
<html lang=""en"">
<head>
<meta charset=""utf-8"">
<title>Farflyg API</title>
<style>
body { margin: 0 auto; padding: 2em; max-width: 48em; font: 15px/1.5em Arial, Sans-Serif; }
header h1 { font-size: 2em; font-weight: normal; }
header h2 { font-size: 1.25em; font-weight: normal; }
h1 { font-size: 1.5em; font-weight: normal; }
</style>
</head>
<body>
<header>
<h1>Farflyg API</h1>
<h2>This API is brought to you with <a href=""https://github.com/VictorBlomberg/Farflyg.Server"">Farflyg.Server</a></h2>
</header>
<h1>Data License</h1>
<p>Farflyg uses data from <a href=""https://developer.jcdecaux.com/"">JCDecaux</a>, licensed with the <a href=""https://developer.jcdecaux.com/files/Open-Licence-en.pdf"">Open Licence</a> from <a href=""http://www.etalab.gouv.fr/pages/licence-ouverte-open-licence-5899923.html"">Etalab</a>.</p>
<p><strong>Is it <em>not</em> allowed to use the data from this API without complying with the Open License above, and properly attributing JCDecaux.</strong></p>
<p>While this API uses data from JCDecaux, this API is neither related to or endorsed by JCDecaux. Data provided by this API may or may not be of the same quality as the original data from JCDecaux.</p>
<p>The data provided by this API is provided ""AS IS"" without warranty of any kind. The data is licensed under <a href=""https://creativecommons.org/licenses/by/2.0/"">CC-BY 2.0</a>.</p>
<p><a href=""https://developer.jcdecaux.com/"">&copy; JCDecaux</a><br><a href=""https://nerven.se/"">&copy; Victor Blomberg and contributors.</a></p>
</body>
</html>
");
                    }));
        }
    }
}
