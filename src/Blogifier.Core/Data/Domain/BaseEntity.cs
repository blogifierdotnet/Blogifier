using System;

namespace Blogifier.Core.Data.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
