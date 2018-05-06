using System;
using System.Runtime.InteropServices;

namespace webview_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Ffi.webview("Foo bar",
            "https://willspeak.me",
            800,
            600,
            1);
        }
    }
}
