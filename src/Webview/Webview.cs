using System;
using System.Drawing;

namespace Webview
{
    using static Ffi;

    public class Webview : IDisposable
    {
        private readonly UIntPtr _webview;

        public static void Simple(string title, IContent content, Size size = default, bool resizable = true)
        {
            if (size == default)
                size = new Size(800, 600);
            webview(title, content.ToUri(), size.Width, size.Height, resizable ? 1 : 0);
        }

        // TODO: make this internal and provide a builder API.
        internal Webview(
            string title,
            IContent content,
            Size size,
            bool resizable,
            bool debug,
            Action<Webview, string> invokeCallback)
        {
            _webview = webview_alloc(
                title,
                content.ToUri(),
                size.Width,
                size.Height,
                resizable ? 1 : 0,
                debug ? 1 : 0,
                null); // TODO: callback support
            if (_webview == UIntPtr.Zero)
                throw new Exception("Could not allocate webview");
        }

        public void Run()
        {
            while (webview_loop(_webview, 1) == 0)
                ;
            webview_exit(_webview);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                webview_release(_webview);

                disposedValue = true;
            }
        }

        ~Webview() {
          Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}