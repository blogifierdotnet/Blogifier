using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Welcome
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Parameter]
        public EventCallback<string> OnUpdate { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        
        public Author Author { get; set; }
        protected int PostCount { get; set; }
        protected int ViewsCount { get; set; }
        protected int DraftCount { get; set; }
        protected bool Edit { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        public async Task Load()
        {
            var authState = await AuthenticationStateTask;

            Author = await DataService.Authors.GetItem(a =>
                a.AppUserName == authState.User.Identity.Name);

            PostCount = DataService.BlogPosts.All().ToList().Count;
            ViewsCount = DataService.BlogPosts.All().Select(v => v.PostViews).Sum();
            DraftCount = DataService.BlogPosts.Find(p => p.Published == DateTime.MinValue).Count();

            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JSRuntime.InvokeAsync<string>("commonJsFunctions.startClock", "");
        }

        protected void EditPost(int id)
        {
            Edit = true;
            StateHasChanged();
        }

        protected async Task HideEditor()
        {
            Edit = false;
            await OnUpdate.InvokeAsync("add");
            StateHasChanged();
        }
    }
}