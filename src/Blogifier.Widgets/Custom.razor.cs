using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sotsera.Blazor.Toaster;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Custom : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        private string FieldKey = "admin-dashboard-sidebar";
        protected bool Visible { get; set; }
        protected string FieldValue { get; set; }

        protected void ShowEditor()
        {
            Visible = true;
            StateHasChanged();
        }

        protected void HideEditor()
        {
            Visible = false;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            FieldValue = DataService.CustomFields.GetCustomValue(FieldKey);
            StateHasChanged();
        }

        protected async Task Save()
        {
            FieldValue = await JSRuntime.InvokeAsync<string>("commonJsFunctions.getEditorValue", "");
            await DataService.CustomFields.SaveCustomValue(FieldKey, FieldValue);

            StateHasChanged();
            Toaster.Success("Updated");
        }
    }
}