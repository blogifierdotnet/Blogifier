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
        protected IConfiguration Configuration { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected string Cover { get; set; }
        protected PostItem Post { get; set; }
        private PostAction Action = PostAction.Save;

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

        protected async Task Save()
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
                        await OnUpdate.InvokeAsync("add"); 
                    }
                    else
                    {
                        var item = await DataService.BlogPosts.GetItem(p => p.Id == Post.Id);

                        item.Content = Post.Content;
                        item.Title = Post.Title;
                        item.Description = Post.Description;
                        item.Categories = Post.Categories;

                        if(Action == PostAction.Unpublish)
                            item.Published = DateTime.MinValue;

                        saved = await DataService.BlogPosts.SaveItem(item);
                    }
                    DataService.Complete();

                    if (Action == PostAction.Publish && !AppSettings.DemoMode)
                    {
                        // send newsletters on post publish
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
                                int count = await EmailService.SendNewsletters(blogPost, emails, "http://blogifier.net");
                                if(count > 0)
                                {
                                    Toaster.Success($"Sent {count} newsletters");
                                }
                            }
                        }
                    }
                    Toaster.Success("Saved");
                    Action = PostAction.Save;
                    Post = saved;
                    StateHasChanged();
                }               
            }
            catch (Exception ex)
            {
                Action = PostAction.Save;
                Toaster.Error(ex.Message);
            }
        }

        protected async Task Publish()
        {
            Post.Published = SystemClock.Now();
            Action = PostAction.Publish;
            await OnUpdate.InvokeAsync("publish");
            await Save();
        }

        protected async Task Unpublish()
        {
            Action = PostAction.Unpublish;
            await OnUpdate.InvokeAsync("unpublish");
            await Save();
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