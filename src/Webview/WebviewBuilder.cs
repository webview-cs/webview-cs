using System;
using System.Drawing;

namespace Webview
{
    public class WebviewBuilder
    {
        private string _title;
        private IContent _content;
        private Size _size;
        private bool _resizable;
        private bool _debug;
        private Action<Webview, string> _callback;

        public WebviewBuilder(string title, IContent content)
        {
            _title = title;
            _content = content;
            _size = default;
            _resizable = true;
            _debug = false;
            _callback = null;
        }

        public WebviewBuilder(Uri uri) :
            this(uri.ToString(), Content.FromUri(uri))
        {}

        public WebviewBuilder(string Title) :
            this(Title, Content.FromHtml(""))
        {}

        public WebviewBuilder() :
            this("", Content.FromHtml(""))
        {}

        public WebviewBuilder WithSize(Size size)
        {
            _size = size;
            return this;
        }

        public WebviewBuilder Resizeable(bool resizable = true)
        {
            _resizable = resizable;
            return this;
        }

        public WebviewBuilder Debug(bool debug = true)
        {
            _debug = debug;
            return this;
        }

        public WebviewBuilder WithInvokeCallback(Action<Webview, string> callback)
        {
            _callback = callback;
            return this;
        }

        public WebviewBuilder WithContent(IContent content)
        {
            _content = content;
            return this;
        }

        
        public WebviewBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public Webview Build()
        {
            var size = _size;
            if (size == default)
                size = new Size(800, 600);
            return new Webview(
                _title,
                _content,
                size,
                _resizable,
                _debug,
                _callback
            );
        }
    }
}