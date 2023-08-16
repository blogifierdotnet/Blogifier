using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blogifier.Admin.Interop;

public class EditorJsInterop : IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> moduleTask;

  public EditorJsInterop(IJSRuntime jsRuntime)
  {
    moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "./admin/js/editor.js").AsTask());
  }

  public async ValueTask LoadEditorAsync(ElementReference? textarea, ElementReference? imageUpload, string toolbar = "fullToolbar")
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("loadEditor", toolbar, textarea, imageUpload);
  }

  public async ValueTask SetEditorValueAsync(string content)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("setEditorValue", content);
  }

  public async ValueTask<string> GetEditorValueAsync()
  {
    var module = await moduleTask.Value;
    var content = await module.InvokeAsync<string>("getEditorValue");
    return content;
  }

  public async ValueTask WriteFrontFileAsync(ElementReference? imageUpload)
  {
    var module = await moduleTask.Value;
    await module.InvokeVoidAsync("writeFrontFile", imageUpload);
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
