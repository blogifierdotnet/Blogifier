using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blogifier.Admin.Interop;

public class CommonJsInterop : IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> moduleTask;

  public CommonJsInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./_content/RazorClassLibrary1/exampleJsInterop.js").AsTask());
  }

  public async ValueTask DisposeAsync()
  {
    if (moduleTask.IsValueCreated)
    {
      var module = await moduleTask.Value;
      await module.DisposeAsync();
    }
  }
}
