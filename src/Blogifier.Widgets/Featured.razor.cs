using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Featured
    {
        [Parameter] public EventCallback<string> OnUpdate { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        IJsonStringLocalizer<Drafts> Localizer { get; set; }
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
            Posts = await Task.FromResult(DataService.BlogPosts.Find(p => p.IsFeatured));
            StateHasChanged();
        }

        public async Task Feature(int id)
        {
            try
            {
                var post = DataService.BlogPosts.Find(p => p.Id == id).FirstOrDefault();
                post.IsFeatured = false;
                DataService.Complete();

                await OnUpdate.InvokeAsync("unfeature");

                StateHasChanged();
                Toaster.Success("Saved");
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