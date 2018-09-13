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
            host.Start();
            var content = new WebHostContent(host);
            builder.WithContent(content).Build().Run();
            host.StopAsync();
        }

        public static void RunWebview(this IWebHost host, string title = "", Size size = default)
        {
            RunWebview(host, new WebviewBuilder().WithTitle(title).WithSize(size));
        }
    }

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder WithDynamicPort(this IWebHostBuilder builder)
        {
            return builder.UseUrls("http://127.0.0.1:0");
        }

        public static IWebHostBuilder WithNoOutput(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging((ILoggingBuilder logBuilder) => { logBuilder.ClearProviders(); });
        }
    }
}
