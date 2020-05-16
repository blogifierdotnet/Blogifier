using Blogifier.Core.Data;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
            await LoadLastWeek();
        }

        async Task LoadLastWeek()
        {
            var stats = await Task.FromResult(DataService.StatsRepository
                .Find(p => p.DateCreated >= SystemClock.Now().Date.AddDays(-7)));

            stats = stats.OrderBy(s => s.DateCreated);

            var chartItems = new List<ChartItem>();

            foreach (var stat in stats)
            {
                var date = stat.DateCreated.ToShortDateString();
                var item = chartItems.Where(i => i.Label == date).FirstOrDefault();

                if (item == null)
                {
                    chartItems.Add(new ChartItem { Label = date, Value = stat.Total });
                }
                else
                {
                    item.Value += stat.Total;
                }
            }

            Labels = chartItems.Select(i => i.Label).ToArray();
            Values = chartItems.Select(i => i.Value).ToArray();
        }

        async Task LoadLatestPosts()
        {
            var posts = await Task.FromResult(DataService.BlogPosts
                .Find(p => p.Published > DateTime.MinValue)
                .OrderByDescending(p => p.Published));

            Labels = posts.Select(p => p.Title.Length > 4 ? p.Title.Substring(0, 5) + ".." : p.Title).ToArray();
            Values = posts.Select(p => (double)p.PostViews).ToArray();

            Array.Reverse(Labels);
            Array.Reverse(Values);

            if (Labels.Length > 10)
            {
                int skip = Labels.Length - 10;
                Values = Values.Skip(skip).ToArray();
                Labels = Labels.Skip(skip).ToArray();
            }
        }
    }

    public class ChartItem
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }
}