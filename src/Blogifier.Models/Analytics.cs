namespace Blogifier.Models
{
    public class Totals
    {
        public int PostCount { get; set; }
        public int ViewsCount { get; set; }
        public int DraftCount { get; set; }
        public int SubsriberCount { get; set; }
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
