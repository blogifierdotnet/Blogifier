using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Drafts
    {
        [Parameter] 
        public EventCallback<string> OnUpdate { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IEmailService EmailService { get; set; }
        [Inject]
        IJsonStringLocalizer<Drafts> Localizer { get; set; }
        [Inject]
        protected IConfiguration Configuration { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected int PostId { get; set; }
        protected bool Edit { get; set; }

        IEnumerable<BlogPost> Posts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        public async Task Load()
        {
            Posts = await Task.FromResult(DataService.BlogPosts.Find(
                p => p.Published == DateTime.MinValue));
            StateHasChanged();
        }

        protected async Task Publish(int id)
        {
            try
            {
                var post = await DataService.BlogPosts.GetItem(p => p.Id == id);
                post.Published = DateTime.UtcNow;
                var saved = await DataService.BlogPosts.SaveItem(post);
                DataService.Complete();

                if (!AppSettings.DemoMode)
                {
                    // send newsletters on post publish
                    var section = Configuration.GetSection(Constants.ConfigSectionKey);
                    if (section != null)
                    {
                        var apiKey = section.GetValue<string>("SendGridApiKey");
                        if (!string.IsNullOrEmpty(apiKey) && apiKey != "YOUR-SENDGRID-API-KEY")
                        {
                            var pager = new Pager(1, 10000);
                            var items = await DataService.Newsletters.GetList(e => e.Id > 0, pager);
                            var emails = items.Select(i => i.Email).ToList();
                            var blogPost = DataService.BlogPosts.Single(p => p.Id == saved.Id);

                            int count = await EmailService.SendNewsletters(blogPost, emails, NavigationManager.BaseUri);
                            if (count > 0)
                            {
                                Toaster.Success(string.Format(Localizer["email-sent-count"], count));
                            }
                        }
                    }
                }
                Toaster.Success("Saved");

                await OnUpdate.InvokeAsync("publish");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        protected void EditPost(int id)
        {
            Edit = true;
            PostId = id;
            StateHasChanged();
        }

        protected async Task HideEditor(string arg)
        {
            Edit = false;
            PostId = 0;
            await OnUpdate.InvokeAsync(arg);
            StateHasChanged();
        }
    }
}
