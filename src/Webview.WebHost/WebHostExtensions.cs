using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Webview.WebHost
{

    internal class WebviewHost : IWebHost
    {
        public IFeatureCollection ServerFeatures => _impl.ServerFeatures;

        public IServiceProvider Services => _impl.Services;

        IWebHost _impl;
        CancellationTokenSource _cts;
        WebviewBuilder _webviewBuilder;
        Webview _webview;


        public WebviewHost(IWebHost webhost, WebviewBuilder webviewBuilder)
        {
            _webviewBuilder = webviewBuilder;
            _impl = webhost;
            _cts = new CancellationTokenSource();
            _webview = _webviewBuilder.Build();

        }

        public void Dispose()
        {
//            _impl.Dispose();
        }

        public void Start()
        {
            StartAsync(_cts.Token);
        }

        public Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Task hostTask = _impl.StartAsync(_cts.Token);
            var features = _impl.ServerFeatures.Get<IServerAddressesFeature>();
            string address = features.Addresses.FirstOrDefault();
            Uri uri = new Uri(address);
            IContent content = Content.FromUri(new Uri($"http://127.0.0.1:{uri.Port}"));

            Thread t = new Thread(() =>
            {
                while (_webview.Loop() == 0)
                    Task.Yield();
                    ;
                _cts.Cancel();
                /*
                new WebviewBuilder("WebApp", content)
                 .WithSize(new Size(1024, 768))
                 .Debug()
                 .Build()
                 .Run();
                 */
            });

            t.Start();

            return hostTask;

            //return hostTask;
//            return hostTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _impl.StopAsync(cancellationToken);
        }
    }

    internal class WebviewWebHostBuilder : IWebHostBuilder
    {
        IWebHostBuilder _impl;
        WebviewBuilder _webviewBuilder;
        public WebviewWebHostBuilder(IWebHostBuilder builder, WebviewBuilder webviewBuilder)
        {
            _impl = builder;
            _webviewBuilder = webviewBuilder;
        }

        public IWebHost Build()
        {
            IWebHost host = _impl.Build();
            var features = host.ServerFeatures.Get<IServerAddressesFeature>();
            string address = features.Addresses.FirstOrDefault();
            Uri uri = new Uri(address);
            IContent content = Content.FromUri(new Uri($"http://127.0.0.1:{uri.Port}"));
            _webviewBuilder.WithContent(content);
            WebviewHost webviewhost = new WebviewHost(host, _webviewBuilder);
            return webviewhost;
        }

        public IWebHostBuilder ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return _impl.ConfigureAppConfiguration(configureDelegate);
        }

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            return _impl.ConfigureServices(configureServices);
        }

        public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        {
            return _impl.ConfigureServices(configureServices);
        }

        public string GetSetting(string key)
        {
            return _impl.GetSetting(key);
        }

        public IWebHostBuilder UseSetting(string key, string value)
        {
            return _impl.UseSetting(key, value);
        }
    }

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
