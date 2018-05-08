# webview-cs

C# Bindings to https://github.com/zserge/webview

## Installation

You can get your hands on Webview from NuGet:

    PM> Install-Package Webview

or from the dotnet command line:

    $ dotnet add package Webview

## Examples

There are two main APIs to create a webview; the simple API, and the builder API. With the simple API all interaction takes place via the `Webview.Webview.Simple` method. This allows you to quickly get a webview running, but doesn't provide the ability to register an invoke callback for JS amongst other things. This is a good starting place.

```cs
using Webview

Webview.Webview.Simple("Window Title", "https://github.com/iwillspeak/webview-cs"))
```

You can also provide an initial window size and control if the window can be resized:

```cs
using Webview;
using System.Drawing;

Webview.Webview.Simple(
  "Title",
  Content.FromHtml("<html><h1>Hello World!"),
  size: new Size(500, 200),
  resizable: true
)
```

For the builder API you first need to create a `WebviewBuilder`. A fluent API allows you to choose how the `Webview` is created. Once built the resulting view must manually be `Run` to display the window.

```cs
using Webview;
using System.Drawing;

new WebviewBuilder(new Uri("http://google.com"))
    .WithSize(new Size(1024, 768))
    .Resizable()
    .Debug()
    .WithInvokeCallback((webview, action) => {
      Console.WriteLine("Action: {0}", action);
    })
    .Build()
    .Run();
```

If you're program is going to outlive the `Webview` you can wrap it in a `using` statement to make sure unmanaged resources are disposed of when you expect them to be:

```cs
using Webview;

using (var webview = new WebviewBuider("Title", Content.FromHtml("<p>Hello World")
                        .WithCallback(MyCallbackFunction))
                        .Build())
{
    webview.Run();
}
```

## Tread Carefully

This is still a work in progress. Native webview binaries are only provided for macOS right now, you'll need to proved your own `libwebview.so` on Linux and `webview.dll` on Windows.

## Feature Status

 * [x] Run webview with standard parameters.
 * [x] Builder API for creating webviews.
 * [ ] Native binaries for Linux, macOS, and Windows.
 * [ ] Option to inject a server to respond to requests (auto binding to random ephemeral port and proxying requests).
  * Maybe this should take an `IWebHostBuilder` or similar from ASP .NET Core.
  * Should this be compatible with OWIN apps too?
