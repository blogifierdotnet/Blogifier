using System.Collections.Generic;

namespace Core.Data
{
    public class WidgetsModel
    {
        public string Name { get; set; }
        public List<Widget> Widgets { get; set; }
    }

    public class Prop
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class Widget
    {
        public string Name { get; set; }
        public List<Prop> Props { get; set; }
    }
}