using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Popular
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        IJsonStringLocalizer<Drafts> Localizer { get; set; }

        protected int PostId { get; set; }
        protected bool Edit { get; set; }

        IEnumerable<BlogPost> Posts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Posts = await Task.FromResult(DataService.BlogPosts.All().OrderByDescending(p => p.PostViews).Take(5));
        }

        protected void EditPost(int id)
        {
            Edit = true;
            PostId = id;
            StateHasChanged();
        }

        protected void HideEditor()
        {
            Edit = false;
            PostId = 0;
            StateHasChanged();
        }
    }
}
