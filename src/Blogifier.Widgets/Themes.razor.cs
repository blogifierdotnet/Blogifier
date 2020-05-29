using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Themes
    {
        [Parameter] 
        public EventCallback<string> OnUpdate { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IStorageService StorageService { get; set; }
        [Inject]
        IJsonStringLocalizer<Themes> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        List<ThemeItem> ThemeItems { get; set; }
        ThemeItem CurrentTheme { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        public async Task Load()
        {
            var blogSettings = await DataService.CustomFields.GetBlogSettings();
            ThemeItems = new List<ThemeItem>();
            foreach (var theme in StorageService.GetThemes())
            {
                if(theme.ToLower() == blogSettings.Theme.ToLower())
                {
                    CurrentTheme = new ThemeItem
                    {
                        Title = theme.Capitalize(),
                        IsCurrent = (blogSettings.Theme.ToLower() == theme.ToLower()),
                        Cover = GetCover(theme)
                    };
                }
                else
                {
                    ThemeItems.Add(new ThemeItem
                    {
                        Title = theme.Capitalize(),
                        IsCurrent = (blogSettings.Theme.ToLower() == theme.ToLower()),
                        Cover = GetCover(theme)
                    });
                }
                
            }

            StateHasChanged();
        }

        protected async Task SelectTheme(string theme)
        {
            var blogSettings = await DataService.CustomFields.GetBlogSettings();
            blogSettings.Theme = theme.ToLower();
            await DataService.CustomFields.SaveBlogSettings(blogSettings);

            Toaster.Success("Saved");
            await Load();
        }

        private string GetCover(string theme)
        {
            string slash = Path.DirectorySeparatorChar.ToString();
            string file = $"{AppSettings.WebRootPath}{slash}themes{slash}{theme}{slash}screenshot.png";
            if (File.Exists(file))
            {
                return $"themes/{theme}/screenshot.png";
            }
            return "admin/img/img-placeholder.png";
        }

    }
}
