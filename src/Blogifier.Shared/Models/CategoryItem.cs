using System;

namespace Blogifier.Shared
{
    public class CategoryItem : IComparable<CategoryItem>
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public DateTime DateCreated { get; set; }

        public int CompareTo(CategoryItem other)
        {
            return Category.ToLower().CompareTo(other.Category.ToLower());
        }
    }
}
