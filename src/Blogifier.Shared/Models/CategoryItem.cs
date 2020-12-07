using System;

namespace Blogifier.Shared
{
	public class CategoryItem : IComparable<CategoryItem>
   {
      public string Category { get; set; }
      public int PostCount { get; set; }

      public int CompareTo(CategoryItem other)
      {
         return Category.ToLower().CompareTo(other.Category.ToLower());
      }
   }
}
