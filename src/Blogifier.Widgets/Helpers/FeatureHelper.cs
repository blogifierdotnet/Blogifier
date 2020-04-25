using Blogifier.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public enum AppFeatureFlags
    {
        Demo,
        Email
    }

    [FilterAlias("EmailFilter")]
    public class EmailFeatureFilter : IFeatureFilter
    {
        private readonly IConfiguration Configuration;

        public EmailFeatureFilter(IConfiguration configuration)
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
            bool.TryParse(featureSection.GetValue<string>(nameof(AppFeatureFlags.Demo)), out isDemo);

            var isConfigured = blogSection != null && 
                blogSection.GetValue<string>("SendGridApiKey") != "YOUR-SENDGRID-API-KEY";

            return Task.FromResult(isConfigured && !isDemo);
        }
    }
}