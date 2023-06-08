using System.Collections.Generic;

namespace Blogifier.Shared
{
	public class Field
   {
      public string Id { get; set; }
      public string Label { get; set; }
      public string Type { get; set; }
      public string Value { get; set; }
      public List<string> Options { get; set; }
   }

   public class Section
   {
      public string Label { get; set; }
      public List<Field> Fields { get; set; }
   }

   public class ThemeSettings
   {
      public List<Section> Sections { get; set; }
   }
}