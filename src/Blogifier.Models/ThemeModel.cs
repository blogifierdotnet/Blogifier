namespace Blogifier.Models
{
    public class ThemeDataModel
    {
        public string Theme { get; set; }
        public string Data { get; set; }
    }

    public class ThemeItem
    {
        public string Title { get; set; }
        public string Cover { get; set; }
        public bool IsCurrent { get; set; }
        public bool HasSettings { get; set; }
    }
}
