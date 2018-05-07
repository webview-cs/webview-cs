# webview-cs

C# Bindings to https://github.com/zserge/webview

# 🐉 Here Be Dragons 🐉

This is a work in progress. The plan is to create C# bindings to szerge/webview
for quickly prototyping web-based UIs.

## Features

 * [*] Run webview with standard parameters.
 * [*] Builder API for creating webviews.
 * [ ] Option to inject a server to respond to requests (auto binding to random ephemeral port and proxying requests).
  * Maybe this should take an `IWebHostBuilder` or similar from ASP .NET Core.
  * Should this be compatible with OWIN apps too?
