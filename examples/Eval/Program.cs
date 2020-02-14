using System;
using System.Drawing;

using Webview;

namespace Eval
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var html = @"<html><body><h1>Eval</h1><div id='content'></div></body></html>";
            var webview = new WebviewBuilder("Eval", Content.FromHtml(html))
                .WithSize(new Size(1024, 768))
                .WithInvokeCallback((view, payload) => Console.WriteLine($"Hello {payload}"))
                .Debug()
                .Build();

            TimeSpan duration = TimeSpan.FromMilliseconds(1000);
            DateTime timeout = DateTime.Now;

            int tick = 0;
            while (webview.Loop() == 0)
            {
                if (DateTime.Now > timeout)
                {
                    timeout = DateTime.Now + duration;
                    webview.Eval(@"document.getElementById('content').textContent = 'Time:" + DateTime.Now.ToString() + "'");
                }

                if (tick % 100 == 0)
                {
                    webview.Eval($@"external.invoke('world @ {tick}')");
                }

                GC.Collect();

                webview.Title = $"Eval - tick:{tick++.ToString()}";
            }
        }
    }
}
