using System.Collections.Generic;

namespace Blogifier.Models
{
    public class WidgetsModel
    {
        public List<WidgetItem> Widgets { get; set; }
    }

    public class WidgetItem
    {
        public string Widget { get; set; }
        public string Title { get; set; }
    }

    public class ThemeWidget
    {
        public string Theme { get; set; }
        public WidgetItem Widget { get; set; }
    }
}