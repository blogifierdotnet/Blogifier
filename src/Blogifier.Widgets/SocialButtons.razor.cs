using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class SocialButtons
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected string SocialValue { get; set; }
        private string SocialKey = "social-buttons-key";

        protected override void OnInitialized()
        {
            SocialValue = DataService.CustomFields.GetCustomValue(SocialKey);
            StateHasChanged();
        }

        protected async Task Save()
        {
            await DataService.CustomFields.SaveCustomValue(SocialKey, SocialValue);
            StateHasChanged();
            Toaster.Success("Updated");
        }
    }
}