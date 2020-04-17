using Microsoft.AspNetCore.Components;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core;
using System.Reflection;

namespace Blogifier.Pages.Admin
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        protected AppDbContext DbContext { get; set; }
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IStorageService StorageService { get; set; }
        [Inject]
        protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        [Inject]
        protected IOptions<JsonLocalizationOptions> Options { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected BlogItem Model { get; set; }
        protected List<SelectListItem> Cultures { get; set; }
        protected List<SelectListItem> Themes { get; set; }
        protected string DbProviderName { get; set; }
        protected string BlazorVersion { get; set; }
        private string CurrentCulture { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Cultures = Options.Value.SupportedCultureInfos
                .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                .ToList();

            Themes = new List<SelectListItem>();
            foreach (var theme in StorageService.GetThemes())
            {
                Themes.Add(new SelectListItem { Value = theme, Text = theme.Capitalize() });
            }

            DbProviderName = DbContext.Database.ProviderName;

            BlazorVersion = typeof(Settings)
                    .GetTypeInfo()
                    .Assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion;

            Model = await DataService.CustomFields.GetBlogSettings();
            CurrentCulture = Model.Culture;
        }

        protected async Task HandleValidSubmit()
        {
            await DataService.CustomFields.SaveBlogSettings(Model);

            if (Model.Culture != CurrentCulture)
            {
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Model.Culture));
                cookieValue = cookieValue.Replace("=", "%3D").Replace("|", "%7C");

                var test = await JSRuntime.InvokeAsync<string>(
                    "commonJsFunctions.writeCookie",
                    CookieRequestCultureProvider.DefaultCookieName,
                    cookieValue,
                    365
                );
            }

            Toaster.Success("Saved");
        }

        protected void Uploaded()
        {
            StateHasChanged();
            Toaster.Success("Saved");
        }
    }
}
