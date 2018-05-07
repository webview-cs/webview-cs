using System;
using System.Drawing;

namespace Webview
{
    using static Ffi;

    public class Webview : IDisposable
    {
        private readonly UIntPtr _webview;

        // TODO: make this internal and provide a builder API.
        public Webview(
            string title,
            string url,
            Size size,
            bool resizable,
            bool debug,
            Action<Webview, string> invokeCallback)
        {
            _webview = webview_alloc(
                title,
                url,
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