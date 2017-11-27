using Blogifier.Core.Common;
using Blogifier.Core.Services.Email;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Blogifier.Core.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailService emailSender, string email, string link)
        {
            return emailSender.Send(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static Task SendEmailWelcomeAsync(this IEmailService emailSender, string email, string name, string link)
        {
            string file = Path.Combine(ApplicationSettings.WebRootPath, @"admin\emails.json");

            using (StreamReader r = new StreamReader(file))
            {
                var json = r.ReadToEnd();
                var obj = JObject.Parse(json);

                var subject = (string)obj["welcome-subject"];
                var body = (string)obj["welcome-body"];

                return emailSender.Send(email, 
                    string.Format(subject, BlogSettings.Title),
                    string.Format(body, name, link));
            }
        }
    }
}
