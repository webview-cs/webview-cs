using System;
using System.Drawing;
using System.Linq;
using System.Threading;
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
            Uri uri = new Uri(address);
            IContent content = Content.FromUri(new Uri($"http://127.0.0.1:{uri.Port}"));
            builder
             .WithContent(content)
             .Build()
             .Run();

            cts.Cancel();
        }
    }
}
