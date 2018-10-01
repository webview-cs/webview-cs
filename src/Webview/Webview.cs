using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
namespace Webview
{
    using System.Diagnostics;
    using static Ffi;

    public class Webview : IDisposable
    {
        private readonly Action<Webview, string> _invokeCallback;
        private readonly UIntPtr _webview;

        /// <summary>
        /// Start a simple webview.
        /// </summary>
        public static void Simple(string title, IContent content, Size size = default, bool resizable = true)
        {
            if (size == default)
                size = new Size(800, 600);
            webview(title, content.ToUri(), size.Width, size.Height, resizable ? 1 : 0);
        }

        internal Webview(
            string title,
            IContent content,
            Size size,
            bool resizable,
            bool debug,
            Action<Webview, string> invokeCallback)
        {
            _invokeCallback = invokeCallback;
            _webview = webview_alloc(
                title,
                content.ToUri(),
                size.Width,
                size.Height,
                resizable ? 1 : 0,
                debug ? 1 : 0,
                InvokeCallback);
            if (_webview == UIntPtr.Zero)
                throw new Exception("Could not allocate webview");
        }

        /// <summary>
        /// Invoke Callback
        /// <para>
        ///  This is used to forward the invoke request on to the
        ///  User's callback if one is registered.
        /// </summary>
        /// <param name="webview">The webview handle</param>
        /// <param name="arg">The opaque invoke argument</param>
        private void InvokeCallback(UIntPtr webview, string arg)
        {
            Debug.Assert(webview == _webview);
            _invokeCallback?.Invoke(this, arg);
        }

        /// <summary>
        /// Get and set the webview's attached userdata. This can later be read
        /// to provide webview-specific actions in callbacks.
        /// </summary>
        public UIntPtr UserData
        {
            set => webview_set_userdata(_webview, value);
            get => webview_get_userdata(_webview);
        }

        /// <summary>
        /// Run the webview main loop. Returns when the window is closed.
        /// </summary>
        public void Run()
        {
            while (webview_loop(_webview, 1) == 0)
                ;
            webview_exit(_webview);
        }

        /// <summary>
        /// Run a single loop 
        /// </summary>
        /// <returns></returns>
        public int Loop()
        {
            Thread.Sleep(1);//let messages process, we are calling webview_loop with blocking=0
            return webview_loop(_webview, 0);
        }

        public string Title
        {
            set => webview_set_title(_webview, value);
        }

        public bool Fullscreen
        {
            set => webview_set_fullscreen(_webview, value ? 1 : 0);
        }

        public int Eval(string js) => webview_eval(_webview, js);

        public int InjectCss(string css) => webview_inject_css(_webview, css);

        public void SetColor(Color c) => webview_set_color(_webview, c.R, c.G, c.B, c.A);

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