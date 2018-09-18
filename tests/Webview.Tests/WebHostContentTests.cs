using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webview.WebHost;

namespace Webview.Tests
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class WebHostContentTests
    {
        private class Startup
        {
            public void Configure(IServiceCollection services)
            {
            }
        }

        [Fact]
        public void CreateContentFromLocalhostBuilder()
        {
            var host = CreateHost("http://localhost:4567");


            var content = new WebHostContent(host);


            Assert.Equal("http://localhost:4567", content.ToUri());
        }

        [Fact]
        public void CreateWithMultipleUrisUsesFirst()
        {
            var host = CreateHost("http://foo.com:1234", "http://localhost:4567");
            

            var content = new WebHostContent(host);


            Assert.Equal("http://foo.com:1234", content.ToUri());
        }

        private static IWebHost CreateHost(params string[] uris)
        {
            return WebHost.CreateDefaultBuilder(Array.Empty<string>())
                .UseStartup<Startup>()
                .UseUrls(uris)
                .Build();
        }
    }
}