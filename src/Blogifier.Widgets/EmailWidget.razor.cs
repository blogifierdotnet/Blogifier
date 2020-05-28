using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sotsera.Blazor.Toaster;
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

        protected EmailModel EmailModel { get; set; }
        protected SendGridModel SendGridModel { get; set; }
        protected MailKitModel MailKitModel { get; set; }

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
            EmailModel = await DataService.CustomFields.GetEmailModel();
            SelectedProvider = EmailModel.SelectedProvider;
            SendGridModel = await DataService.CustomFields.GetSendGridModel();
            MailKitModel = await DataService.CustomFields.GetMailKitModel();
        }

        protected void OnProviderChange()
        {
            EmailModel.SelectedProvider = SelectedProvider;
        }

        protected async Task OnSendGridModelSave()
        {
            if (Verified())
            {
                SendGridModel.Configured = false;
                await DataService.CustomFields.SaveSendGridModel(SendGridModel);
                await DataService.CustomFields.SaveEmailModel(EmailModel);
                Toaster.Success(Localizer["completed"]);
            }
        }

        protected async Task OnMailKitModelSave()
        {
            if (Verified())
            {
                MailKitModel.Configured = false;
                await DataService.CustomFields.SaveMailKitModel(MailKitModel);
                await DataService.CustomFields.SaveEmailModel(EmailModel);
                Toaster.Success(Localizer["completed"]);
            }
        }
        
        protected async Task OnCheck()
        {
            if (Verified())
            {
                EmailFactory factory = new EmailService(DataService);
                var emailService = factory.GetEmailService();

                var msg = await emailService.SendEmail(EmailModel.FromName, EmailModel.FromEmail, EmailModel.SendTo, "test subject", "test content");
                bool status = string.IsNullOrEmpty(msg);

                if (EmailModel.SelectedProvider == EmailProvider.MailKit)
                {
                    MailKitModel.Configured = status;
                    await DataService.CustomFields.SaveMailKitModel(MailKitModel);
                }

                if (EmailModel.SelectedProvider == EmailProvider.SendGrid)
                {
                    SendGridModel.Configured = status;
                    await DataService.CustomFields.SaveSendGridModel(SendGridModel);
                }

                if (status) Toaster.Success(Localizer["email-sent-success"]);
                else Toaster.Error(msg);
            }
        }

        bool Verified()
        {
            if (string.IsNullOrEmpty(EmailModel.FromEmail) || string.IsNullOrEmpty(EmailModel.FromName))
            {
                Toaster.Error(Localizer["name-and-email-required"]);
                return false;
            }
            return true;
        }
    }
}
