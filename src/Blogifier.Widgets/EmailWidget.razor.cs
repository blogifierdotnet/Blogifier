using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
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
        protected IStorageService StorageService { get; set; }
        [Inject]
        protected ILogger<EmailService> Logger { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailWidget> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected EmailModel Model { get; set; }
        protected string SendTo { get; set; }

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

        protected async Task OnCheck()
        {
            EmailFactory factory = new EmailService(DataService);
            var emailService = factory.GetEmailService();

            var status = await emailService.SendEmail("admin", "admin@blog.com", SendTo, "test subject", "test content");

            if (Model.SelectedProvider == EmailProvider.MailKit)
                Model.MailKitModel.Configured = status;
                    
            if (Model.SelectedProvider == EmailProvider.SendGrid)
                Model.SendGridModel.Configured = status;

            await DataService.CustomFields.SaveEmailModel(Model);

            if(status) Toaster.Success(Localizer["completed"]);
            else Toaster.Error(Localizer["error"]);

            await Task.FromResult("ok");
        }
    }
}
