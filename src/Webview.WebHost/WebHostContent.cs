using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Webview.WebHost
{
    public class WebHostContent : IContent
    {
        private string _address;
        public WebHostContent(IWebHost host)
        {
            var features = host.ServerFeatures.Get<IServerAddressesFeature>();
            _address = features.Addresses.FirstOrDefault();
        }

        public string ToUri() => _address;
    }
}