using System;
using System.Text;

namespace Webview
{
    public class Content : IContent
    {
        private readonly string _rawUri;

        public static IContent FromHtml(string html)
        {
            var dataUri = new StringBuilder("data:text/html;charset=UTF-8;base64,");
            dataUri.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(html)));
            return new Content(dataUri.ToString());
        }

        private Content(string rawUri)
        {
            _rawUri = rawUri;
        }

        public string ToUri() => _rawUri;
    }
}