using System;
using Xunit;

namespace Webview.Tests
{
    public class ContentTests
    {
        [Fact]
        public void HtmlContentIsEncoded()
        {
            var content = Content.FromHtml("<p>foo");
            Assert.Equal("data:text/html;charset=UTF-8;base64,PHA+Zm9v", content.ToUri());
        }

        [Fact]
        public void UrlContentUsesOriginalString()
        {
            var content = Content.FromUri(new Uri("http://example.com:8080/test"));
            Assert.Equal("http://example.com:8080/test", contnet.ToUri());
        }
    }
}
