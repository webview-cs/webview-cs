using System;
using System.Runtime.InteropServices;

namespace webview_cs
{
    class Program
    {
        // WEBVIEW_API int webview(const char *title, const char *url, int width,
        //                 int height, int resizable);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        static extern int webview([MarshalAs(UnmanagedType.LPUTF8Str)] string title,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string url,
        int width,
         int height,
         int resizable);

        static void Main(string[] args)
        {
            webview("Foo bar",
            "https://willspeak.me",
            800,
            600,
            1);
        }
    }
}
