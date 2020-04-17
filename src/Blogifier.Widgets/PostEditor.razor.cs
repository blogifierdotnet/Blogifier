using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Sotsera.Blazor.Toaster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class PostEditor : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Parameter]
        public int PostId { get; set; }
        [Parameter]
        public EventCallback<string> HideCallback { get; set; }       
        [Parameter] 
        public EventCallback<string> OnUpdate { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IEmailService EmailService { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailForm> Localizer { get; set; }
        [Inject]
        protected IConfiguration Configuration { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected string Cover { get; set; }
        protected PostItem Post { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (PostId > 0)
            {
                Post = await DataService.BlogPosts.GetItem(p => p.Id == PostId);
            }
            else
            {
                var blog = await DataService.CustomFields.GetBlogSettings();
                Post = new PostItem { Cover = blog.Cover, Content = "", Title = "" };
            }
            Cover = $"background-image: url({AppSettings.SiteRoot}{Post.Cover})";
            StateHasChanged();
        }

        protected async Task SavePost(PostAction postAction)
        {
            try
            {
                var content = await JSRuntime.InvokeAsync<string>("commonJsFunctions.getEditorValue", "");
                Post.Content = content;

                if (string.IsNullOrEmpty(Post.Title))
                {
                    Toaster.Error("Post title required");
                }
                else if (string.IsNullOrEmpty(Post.Content))
                {
                    Toaster.Error("Post content required");
                }
                else
                {
                    PostItem saved;
                    if (Post.Id == 0)
                    {
                        var authState = await AuthenticationStateTask;
                        var author = await DataService.Authors.GetItem(
                            a => a.AppUserName == authState.User.Identity.Name);

                        Post.Author = author;
                        Post.Slug = Post.Title.ToSlug();
                        Post.Description = Post.Title;

                        saved = await DataService.BlogPosts.SaveItem(Post);
                    }
                    else
                    {
                        var item = await DataService.BlogPosts.GetItem(p => p.Id == Post.Id);

                        item.Content = Post.Content;
                        item.Title = Post.Title;
                        item.Description = Post.Description;
                        item.Categories = Post.Categories;

                        if(postAction == PostAction.Unpublish)
                            item.Published = DateTime.MinValue;

                        saved = await DataService.BlogPosts.SaveItem(item);
                    }
                    DataService.Complete();

                    if (postAction == PostAction.Publish && !AppSettings.DemoMode)
                    {
                        var section = Configuration.GetSection(Constants.ConfigSectionKey);
                        if(section != null)
                        {
                            var apiKey = section.GetValue<string>("SendGridApiKey");
                            if (!string.IsNullOrEmpty(apiKey) && apiKey != "YOUR-SENDGRID-API-KEY")
                            {
                                var pager = new Pager(1, 10000);
                                var items = await DataService.Newsletters.GetList(e => e.Id > 0, pager);
                                var emails = items.Select(i => i.Email).ToList();
                                var blogPost = DataService.BlogPosts.Single(p => p.Id == saved.Id);

                                int count = await EmailService.SendNewsletters(blogPost, emails, NavigationManager.BaseUri);
                                if(count > 0)
                                {
                                    Toaster.Success(string.Format(Localizer["email-sent-count"], count));
                                }
                            }
                        }
                    }
                    
                    Toaster.Success("Saved");
                    Post = saved;
                    PostId = Post.Id;
                    StateHasChanged();
                }               
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        protected async Task Publish()
        {
            Post.Published = SystemClock.Now();
            await OnUpdate.InvokeAsync("publish");
            await SavePost(PostAction.Publish);
        }

        protected async Task Unpublish()
        {
            await OnUpdate.InvokeAsync("unpublish");
            await SavePost(PostAction.Unpublish);
        }

        protected async Task Save()
        {
            await OnUpdate.InvokeAsync("save");
            await SavePost(PostAction.Save);
        }

        protected async Task Remove(int id)
        {
            try
            {
                var post = DataService.BlogPosts.Find(p => p.Id == id).FirstOrDefault();
                DataService.BlogPosts.Remove(post);
                DataService.Complete();
                
                await HideCallback.InvokeAsync("remove");

                Toaster.Success("Removed");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }
    }

    public enum PostAction
    {
        Save, Publish, Unpublish
    }
}