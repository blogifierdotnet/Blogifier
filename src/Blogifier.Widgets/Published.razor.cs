using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
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
        protected int CurrentPage { get; set; }
        protected bool Edit { get; set; }

        protected IEnumerable<PostItem> Posts { get; set; }
        protected Pager Pager { get; set; }

        protected string FilterLabel { get; set; }
        protected PublishedStatus FilterValue { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        IJsonStringLocalizer<Published> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentPage = 1;
            FilterLabel = Localizer["all"];
            await LoadPosts();
        }

        protected async Task SearchKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchPosts();
            }
        }

        public async Task LoadPage(int page)
        {
            CurrentPage = page;
            await LoadPosts();
        }

        protected async Task SearchPosts()
        {
            CurrentPage = 1;
            await LoadPosts();
        }

        public async Task LoadPosts()
        {
            try
            {
                var blog = await DataService.CustomFields.GetBlogSettings();
                Pager = new Pager(CurrentPage, blog.ItemsPerPage);

                string include = "D,F,P";
                if (FilterValue == PublishedStatus.Drafts) include = "D";
                if (FilterValue == PublishedStatus.Published) include = "F,P";
                if (FilterValue == PublishedStatus.Featured) include = "F";

                if (string.IsNullOrEmpty(SearchTerm)) 
                {
                    Posts = await DataService.BlogPosts.GetList(Pager, 0, "", include);
                }
                else
                {
                    Posts = await DataService.BlogPosts.Search(Pager, SearchTerm, 0, include);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public async Task Publish(int id, bool published)
        {
            try
            {
                var post = DataService.BlogPosts.Find(p => p.Id == id).FirstOrDefault();
                if (published)
                {
                    post.Published = DateTime.MinValue;
                    post.IsFeatured = false;
                }
                else
                {
                    post.Published = SystemClock.Now();
                }

                await Task.FromResult(DataService.Complete());
                await OnUpdate.InvokeAsync("unpublish");

                StateHasChanged();
                Toaster.Success("Saved");
                await LoadPosts();
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
                if (featured)
                {
                    post.IsFeatured = false;
                }
                else
                {
                    post.IsFeatured = true;
                }
                await Task.FromResult(DataService.Complete());
                await OnUpdate.InvokeAsync("feature");

                StateHasChanged();
                Toaster.Success("Saved");
                await LoadPosts();
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        public async Task Filter(PublishedStatus filter)
        {
            FilterValue = filter;
            switch (filter)
            {
                case PublishedStatus.Published:
                    FilterLabel = Localizer["published"];
                    break;
                case PublishedStatus.Drafts:
                    FilterLabel = Localizer["draft", true];
                    break;
                case PublishedStatus.Featured:
                    FilterLabel = Localizer["featured"];
                    break;
                default:
                    FilterLabel = Localizer["all"];
                    break;
            }
            CurrentPage = 1;
            await LoadPosts();
        }

        public void CheckAll(object checkValue)
        {
            bool isChecked = (bool)checkValue;
            Posts.ToList().ForEach(e => e.Selected = isChecked);
        }

        public async Task GroupAction(GroupAction action)
        {
            var selectedPosts = Posts.Where(p => p.Selected).ToList();
            if(selectedPosts.Any())
            {
                bool confirmed = false;
                foreach (var item in selectedPosts)
                {
                    var post = DataService.BlogPosts.Find(p => p.Id == item.Id).FirstOrDefault();
                    switch (action)
                    {
                        case Core.Data.GroupAction.Publish:
                            post.Published = SystemClock.Now();
                            break;
                        case Core.Data.GroupAction.Unpublish:
                            post.Published = DateTime.MinValue;
                            break;
                        case Core.Data.GroupAction.Feature:
                            post.IsFeatured = true;
                            break;
                        case Core.Data.GroupAction.Delete:
                            if (!confirmed)
                            {
                                confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"{Localizer["confirm-delete"]}");
                                if (confirmed)
                                {
                                    DataService.BlogPosts.Remove(post);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    DataService.Complete();
                }
                Toaster.Success(Localizer["completed"]);
                await LoadPosts();
            }
        }
    }
}
