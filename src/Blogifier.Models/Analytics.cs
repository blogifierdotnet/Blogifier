namespace Blogifier.Models
{
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
