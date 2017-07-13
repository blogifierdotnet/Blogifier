using Blogifier.Core.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Core.Services.Social
{
    public class SocialService : ISocialService
    {
        public Task<Dictionary<string, string>> GetSocialButtons(Profile profile)
        {
            var buttons = new Dictionary<string, string>();

            buttons.Add("social-google", "https://plus.google.com/u/0/");
            buttons.Add("social-twitter", "https://twitter.com/blogifierdotnet");
            buttons.Add("social-github", "https://github.com/blogifierdotnet");
            buttons.Add("social-instagram", "https://www.instagram.com");
            buttons.Add("social-facebook", "https://www.facebook.com/blogifierdotnet");

            return Task.Run(()=> buttons);
        }
    }
}
