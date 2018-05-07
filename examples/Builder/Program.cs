using System;
using System.Drawing;

using Webview;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebviewBuilder(new Uri("http://google.com"))
                .WithSize(new Size(1024, 768))
                .Debug()
                .Build()
                .Run();
        }
    }
}
