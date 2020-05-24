using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using Sotsera.Blazor.Toaster.Core;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class EmailWidget : ComponentBase
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailForm> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected EmailModel Model { get; set; }

        private EmailProvider selectedProvider;
        public EmailProvider SelectedProvider
        {
            get { return selectedProvider; }
            set
            {
                selectedProvider = value;
                OnProviderChange();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Model = await DataService.CustomFields.GetEmailModel();
            SelectedProvider = Model.SelectedProvider;
        }

        protected void OnProviderChange()
        {
            Model.SelectedProvider = SelectedProvider;
        }

        protected async Task OnSave()
        {
            if(await DataService.CustomFields.SaveEmailModel(Model))
            {
                Toaster.Success(Localizer["completed"]);
            }
            else
            {
                Toaster.Error(Localizer["error"]);
            }
        }
    }
}
