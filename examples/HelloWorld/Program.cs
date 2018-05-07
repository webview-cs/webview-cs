using System;
using System.Drawing;
using System.Text;
using Webview;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = @"<html><body><h1>hello ü world</h1><script>window.external.invoke(""hello ümlaß world"")</script>";
            var uri = "data:text/html;charset=UTF-8;base64," + Convert.ToBase64String(Encoding.UTF8.GetBytes(html));
            using (var webview = new Webview.Webview(
                "Hello World",
                uri,
                new Size(1024, 768),
                true,
                false,
                null
            ))
            {
                webview.Run();
            }
        }
    }
}
