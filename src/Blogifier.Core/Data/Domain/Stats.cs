using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogifier.Core.Data
{
    public class StatsTotal
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int Total { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }
    }
}