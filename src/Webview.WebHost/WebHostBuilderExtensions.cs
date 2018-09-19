using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Webview.WebHost
{
    /// <summary>
    /// Useful Extesions for <see cref="IWebHostBuilder" />
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// Use a URI with a dynamic local port.
        /// <para>
        ///  This is useful when running a <see cref="Webview" /> to avoid
        ///  port collisions, and allow mutlitple instances of the app to
        ///  be run in parallel.
        /// </para>
        /// </summary>
        /// <param name="builder">The builder to update the URLs in</param>
        /// <returns>The <paramref name="builder" /></returns>
        public static IWebHostBuilder WithDynamicPort(this IWebHostBuilder builder)
        {
            return builder.UseUrls("http://127.0.0.1:0");
        }

        /// <summary>
        /// Disable All Logging for a <see cref="IWebHost" />
        /// </summary>
        /// <param name="builder">The builder to update logging in</param>
        /// <returns>The <paramref name="builder" /></returns>
        public static IWebHostBuilder WithNoOutput(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging((logBuilder) => logBuilder.ClearProviders());
        }
    }
}