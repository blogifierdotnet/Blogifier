using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class MainChart
    {
        [Inject]
        protected IDataService DataService { get; set; }

        protected string[] Labels { get; set; }
        protected double[] Values { get; set; }       

        protected override async Task OnInitializedAsync()
        {
            var posts = await Task.FromResult(DataService.BlogPosts
                .Find(p => p.Published > DateTime.MinValue)
                .OrderByDescending(p => p.Published));

            Labels = posts.Select(p => p.Title.Length > 4 ? p.Title.Substring(0, 5) + ".." : p.Title).ToArray();
            Values = posts.Select(p => (double)p.PostViews).ToArray();

            Array.Reverse(Labels);
            Array.Reverse(Values);

            if(Labels.Length > 10)
            {
                int skip = Labels.Length - 10;
                Values = Values.Skip(skip).ToArray();
                Labels = Labels.Skip(skip).ToArray();
            }
        }
    }
}