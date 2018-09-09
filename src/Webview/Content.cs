using System;
using System.Net;
using System.Text;

namespace Webview
{
    public class Content : IContent
    {
        private readonly string _rawUri;

        public static IContent FromHtml(string html)
        {
            var dataUri = new StringBuilder("data:text/html,");
            dataUri.Append(Uri.EscapeDataString(html));
            return new Content(dataUri.ToString());
        }

        public static IContent FromUri(Uri uri)
        {
            return new Content(uri.ToString());
        }

        private Content(string rawUri)
        {
            _rawUri = rawUri;
        }

        public string ToUri() => _rawUri;
    }
}