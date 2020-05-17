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

        protected List<ChartOption> ChartOptions = new List<ChartOption>() { 
            new ChartOption { Id = ChartSelector.Week, Label = "Last week" },
            new ChartOption { Id = ChartSelector.Month, Label = "Last month" },
            new ChartOption { Id = ChartSelector.All, Label = "All" }
        };

        private ChartSelector selectedChart;
        public ChartSelector SelectedChartOption
        {
            get { return selectedChart; }
            set
            {
                selectedChart = value;
                Load();
            }
        }

        protected override void OnInitialized()
        {
            SelectedChartOption = ChartSelector.Week;
        }

        void Load()
        {
            if (selectedChart == ChartSelector.Week)
            {
                LoadWeekly();
            }
            else if (selectedChart == ChartSelector.Month)
            {
                LoadMonthly();
            }
            else
            {
                LoadAll();
            }
        }

        void LoadWeekly()
        {
            var stats = GetStats(-7);
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

        void LoadMonthly()
        {
            var stats = GetStats(-31);
            var chartItems = new List<ChartItem>();

            foreach (var stat in stats)
            {
                var date = $"{stat.DateCreated.Month}/{stat.DateCreated.Day}";
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

        void LoadAll()
        {
            var stats = GetStats();
            var chartItems = new List<ChartItem>();

            foreach (var stat in stats)
            {
                var date = $"{stat.DateCreated.Month}/{stat.DateCreated.Day}";
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

        IEnumerable<StatsTotal> GetStats(int days = 0)
        {
            IEnumerable<StatsTotal> stats = days == 0 ? DataService.StatsRepository.All() :
                DataService.StatsRepository.Find(p => p.DateCreated >= SystemClock.Now().Date.AddDays(days));
            return stats.OrderBy(s => s.DateCreated);
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

    public class ChartOption
    {
        public ChartSelector Id { get; set; }
        public string Label { get; set; }
    }

    public enum ChartSelector
    {
        Week = 1, Month = 2, All = 3
    }
}