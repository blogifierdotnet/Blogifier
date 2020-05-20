using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace Blogifier.Core.Services
{
    [FilterAlias("EmailConfigured")]
    public class EmailConfiguredFilter : IFeatureFilter
    {
        private readonly IConfiguration Configuration;

        public EmailConfiguredFilter(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Allows to enable or disable email features
        /// </summary>
        /// <param name="context">Feature context</param>
        /// <returns>True if email is configured</returns>
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var blogSection = Configuration.GetSection(Constants.ConfigSectionKey);
            var featureSection = Configuration.GetSection("FeatureManagement");

            bool isDemo;
            bool.TryParse(featureSection.GetValue<string>(nameof(AppFeatureFlags.DemoMode)), out isDemo);

            string keyValue = blogSection.GetValue<string>("SendGridApiKey");

            var isConfigured = blogSection != null 
                && !string.IsNullOrEmpty(keyValue) 
                && keyValue != "YOUR-SENDGRID-API-KEY";

            return Task.FromResult(isConfigured && !isDemo);
        }
    }
}
