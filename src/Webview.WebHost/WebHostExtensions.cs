using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Webview.WebHost
{
    public static class WebHostExtensions
    {
        public static void RunWebview(this IWebHost host, WebviewBuilder builder)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            host.StartAsync(cts.Token);
            var features = host.ServerFeatures.Get<IServerAddressesFeature>();
            string address = features.Addresses.FirstOrDefault();
            IContent content = Content.FromUri(new Uri(address));
            builder.WithContent(content).Build().Run();
            cts.Cancel();
        }

        public static void RunWebview(this IWebHost host, string Title = "", Size size = default(Size))
        {
            RunWebview(host, new WebviewBuilder().WithTitle(Title).WithSize(size));
        }
    }

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureForWebview(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging((ILoggingBuilder logBuilder) => { logBuilder.ClearProviders(); })
                .UseUrls("http://127.0.0.1:0");
        }
    }
}
