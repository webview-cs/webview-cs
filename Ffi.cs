using System;
using System.Runtime.InteropServices;

namespace webview_cs
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void webview_external_invoke_cb_t(
        IntPtr webview,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string arg);

    internal static class Ffi
    {
        // WEBVIEW_API int webview(const char *title, const char *url, int width,
        //                 int height, int resizable);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int webview(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string title,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string url,
            int width,
            int height,
            int resizable);
        
        // WEBVIEW_API struct webview* webview_alloc(const char* title, const char* url,
        //                                           int width, int height,
        //                                           int resizeable, int debug,
        //                                           webview_external_invoke_cb_t cb);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern UIntPtr webview_alloc(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string title,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string url,
            int width,
            int height,
            int resizable,
            int debug,
            webview_external_invoke_cb_t cb);
        // WEBVIEW_API void webview_release(struct webview* webview);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void webview_release(UIntPtr webview);

        // WEBVIEW_API int webview_init(struct webview *w);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int webview_init(UIntPtr webview);
        
        // WEBVIEW_API int webview_loop(struct webview *w, int blocking);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int webview_loop(UIntPtr webview, int blocking);

        // WEBVIEW_API int webview_eval(struct webview *w, const char *js);
        // WEBVIEW_API int webview_inject_css(struct webview *w, const char *css);
        // WEBVIEW_API void webview_set_title(struct webview *w, const char *title);
        // WEBVIEW_API void webview_set_fullscreen(struct webview *w, int fullscreen);
        // WEBVIEW_API void webview_set_color(struct webview *w, uint8_t r, uint8_t g,
        //                                    uint8_t b, uint8_t a);
        // WEBVIEW_API void webview_dialog(struct webview *w,
        //                                 enum webview_dialog_type dlgtype, int flags,
        //                                 const char *title, const char *arg,
        //                                 char *result, size_t resultsz);
        // WEBVIEW_API void webview_dispatch(struct webview *w, webview_dispatch_fn fn,
        //                                   void *arg);
        // WEBVIEW_API void webview_terminate(struct webview *w);
        // WEBVIEW_API void webview_exit(struct webview *w);
        [DllImport("webview", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void webview_exit(UIntPtr webview);
        // WEBVIEW_API void webview_debug(const char *format, ...);
        // WEBVIEW_API void webview_print_log(const char *s);
    }
}