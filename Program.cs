using System;
using System.Runtime.InteropServices;

namespace webview_cs
{
    using static Ffi;

    class Program
    {
        static void Main(string[] args)
        {
            var webview = webview_alloc(
                "Foo bar",
                "https://google.com",
                1024,
                768,
                1,
                0,
                new webview_external_invoke_cb_t(WebviewCallback));
            if (webview == UIntPtr.Zero)
                throw new Exception("Could not allocate webview");
            Console.WriteLine("Got webview: {0}", webview);
            while (webview_loop(webview, 1) == 0)
                ;
            webview_exit(webview);
            webview_release(webview);
        }

        private static void WebviewCallback(IntPtr webview, string arg)
        {
            Console.WriteLine("Webview Callback: {0}", arg);
        }
    }
}
