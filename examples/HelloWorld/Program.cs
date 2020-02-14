using System;
using System.Drawing;
using System.Text;
using Webview;

namespace HelloWorld
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var html = @"<html><body><h1>Hello World</h1>";
            Webview.Webview.Simple("Hello World", Content.FromHtml(html));
        }
    }
}
