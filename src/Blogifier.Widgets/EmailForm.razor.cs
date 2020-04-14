using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Microsoft.Extensions.Configuration;
using Blogifier.Core;

namespace Blogifier.Widgets
{
    public partial class EmailForm : ComponentBase
    {
        [Inject]
        protected IEmailService EmailService { get; set; }
        [Inject]
        protected IConfiguration Configuration { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailForm> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected EmailModel Model { get; set; }

        protected override void OnInitialized()
        {
            Model = new EmailModel { SendTo = "", Subject = "", Content = "" };

            var section = Configuration.GetSection(Constants.ConfigSectionKey);
            if(section != null && !AppSettings.DemoMode)
                Model.ApiKey = section.GetValue<string>("SendGridApiKey");
            else
                Model.ApiKey = "YOUR-SENDGRID-API-KEY";
        }

        protected async Task Send()
        {
            if (AppSettings.DemoMode)
            {
                Toaster.Info(Localizer["demo-disabled"]);
            }
            else
            {
                bool result = await EmailService.SendEmail(Model.SendTo, Model.Subject, Model.Content);
                if (result) Toaster.Success(Localizer["email-sent-success"]);
                else Toaster.Error(Localizer["email-sent-error"]);
            }
        }
    }

    public  class EmailModel
    {
        [Required]
        [EmailAddress]
        public string SendTo { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        public string ApiKey { get; set; }
    }
}
