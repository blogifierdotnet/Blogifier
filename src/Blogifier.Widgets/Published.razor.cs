using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Published : ComponentBase
    {
        [Parameter] public EventCallback<string> OnUpdate { get; set; }

        protected string SearchTerm { get; set; }
        protected int PostId { get; set; }
        protected bool Edit { get; set; }

        protected IEnumerable<PostItem> Posts { get; set; }
        protected Pager Pager { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        IJsonStringLocalizer<Drafts> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPosts(1);
        }

        protected async Task SearchKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchPosts();
            }
        }

        protected async Task SearchPosts()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                await LoadPosts(1);
            }
            else
            {
                var blog = await DataService.CustomFields.GetBlogSettings();
                Pager = new Pager(1, blog.ItemsPerPage);
                Posts = await DataService.BlogPosts.Search(Pager, SearchTerm, 0, "F,P");

                if (Pager.ShowOlder) Pager.LinkToOlder = $"admin/posts?page={Pager.Older}";
                if (Pager.ShowNewer) Pager.LinkToNewer = $"admin/posts?page={Pager.Newer}";
            }
        }

        public async Task LoadPosts(int page)
        {
            try
            {
                var blog = await DataService.CustomFields.GetBlogSettings();
                Pager = new Pager(page, blog.ItemsPerPage);
                Posts = await DataService.BlogPosts.GetList(p => p.Published > DateTime.MinValue, Pager);

                if (Pager.ShowOlder) Pager.LinkToOlder = $"admin/posts?page={Pager.Older}";
                if (Pager.ShowNewer) Pager.LinkToNewer = $"admin/posts?page={Pager.Newer}";
                StateHasChanged();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void EditPost(int id)
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

        public async Task Publish(int id, bool published)
        {
            try
            {
                var post = DataService.BlogPosts.Find(p => p.Id == id).FirstOrDefault();
                post.Published = published ? DateTime.MinValue : DateTime.UtcNow;
                await Task.FromResult(DataService.Complete());

                await OnUpdate.InvokeAsync("unpublish");

                StateHasChanged();
                Toaster.Success("Saved");
                await LoadPosts(1);
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        public async Task Feature(int id, bool featured)
        {
            try
            {
                var post = DataService.BlogPosts.Find(p => p.Id == id).FirstOrDefault();
                post.IsFeatured = !featured;
                await Task.FromResult(DataService.Complete());

                await OnUpdate.InvokeAsync("feature");

                StateHasChanged();
                Toaster.Success("Saved");
                await LoadPosts(1);
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }
    }
}
