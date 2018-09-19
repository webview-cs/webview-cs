using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Webview.WebHost
{
    /// <summary>
    /// Web Host Content
    /// <para>
    ///  A content provider for <see cref="Webview" /> which renders a given
    ///  ASP .NET Core application. This just connects to the first URI in
    ///  the server and doesn't take controll of running the webserver.
    ///  You should probably use <see cref="WebHostExtensions.RunWebview" />
    ///  rather than providing a <see cref="WebHostContent" /> directly.static
    /// </para>
    /// </summary>
    public class WebHostContent : IContent
    {
        private string _address;

        /// <summary>
        /// Create a Content Provider for the given <see cref="IWebHost" />.
        /// </summary>
        /// <param name="host">The host to connect to</param>
        public WebHostContent(IWebHost host)
        {
            var features = host.ServerFeatures.Get<IServerAddressesFeature>();
            _address = features.Addresses.FirstOrDefault();
        }

        /// <summary>
        /// Get a URI for the wrapped <see cref="IWebHost" />
        /// </summary>
        /// <returns>The frist address in the server.</returns>
        public string ToUri() => _address;
    }
}