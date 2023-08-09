using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blogifier.Admin.Interop;

public class CommonJsInterop : IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> moduleTask;

  public CommonJsInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./admin/js/common.js").AsTask());
  }

  public async ValueTask SetTooltipAsync()
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("setTooltip");
  }

  public async ValueTask SetTitleAsync(string content)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("setTitle", content);
  }

  public async ValueTask TriggerClickAsync(ElementReference? element)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("triggerClick", element);
  }

  public async ValueTask<FrontBlobInfo> GetInputFileBlobInfoAsync(ElementReference? inputUpload)
  {
    var module = await moduleTask.Value;
    return await module.InvokeAsync<FrontBlobInfo>("getInputFileBlobInfo", inputUpload);
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
