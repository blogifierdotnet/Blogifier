using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class MdEditor
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Parameter] public string Content { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<string>("commonJsFunctions.loadEditor", "");
            }
                
            await JSRuntime.InvokeAsync<string>("commonJsFunctions.setEditorValue", Content);
        }
    }
}