using System.Runtime.InteropServices;

namespace webview_cs
{
    public static class Ffi
    {
        // WEBVIEW_API int webview(const char *title, const char *url, int width,
        //                 int height, int resizable);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        public static extern int webview(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string title,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string url,
            int width,
            int height,
            int resizable);
    }
}