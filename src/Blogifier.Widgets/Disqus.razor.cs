using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Disqus : ComponentBase
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailForm> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected string DisqusValue { get; set; }
        private readonly string DisqusKey = "disqus-key";

        protected override void OnInitialized()
        {
            DisqusValue = DataService.CustomFields.GetCustomValue(DisqusKey);
            StateHasChanged();
        }

        protected async Task Save()
        {
            await DataService.CustomFields.SaveCustomValue(DisqusKey, DisqusValue);
            StateHasChanged();
            Toaster.Success("Updated");
        }
    }
}