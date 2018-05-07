namespace Webview
{
    public interface IContent
    {
        /// <summary>
        /// Convert Content to Uri ready to be rendered.
        /// <summary>
        string ToUri();
    }
}