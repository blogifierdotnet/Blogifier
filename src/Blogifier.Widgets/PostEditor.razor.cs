using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected string Cover { get; set; }
        protected BlogPost Post { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (PostId > 0)
            {
                Post = DataService.BlogPosts.Find(p => p.Id == PostId).FirstOrDefault();
            }
            else
            {
                var blog = await DataService.CustomFields.GetBlogSettings();
                Post = new BlogPost { Cover = blog.Cover, Content = "", Title = "" };
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
                    if (Post.Id == 0)
                    {
                        var authState = await AuthenticationStateTask;
                        var author = await DataService.Authors.GetItem(
                            a => a.AppUserName == authState.User.Identity.Name);

                        Post.AuthorId = author.Id;
                        Post.Slug = Post.Title.ToSlug();
                        Post.Description = Post.Title;

                        await OnUpdate.InvokeAsync("add");

                        DataService.BlogPosts.Add(Post);
                        DataService.Complete();
                    }
                    else
                    {
                        var item = await DataService.BlogPosts.GetItem(p => p.Id == Post.Id);

                        item.Content = Post.Content;
                        item.Title = Post.Title;

                        await DataService.BlogPosts.SaveItem(item);
                    }
                    Toaster.Success("Saved");
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
            await Save();
        }

        protected async Task Unpublish()
        {
            Post.Published = DateTime.MinValue;
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
}