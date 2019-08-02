using Core.Helpers;
using System.Collections.Generic;

namespace Core.Data.Models
{
    public class NewsletterModel
    {
        public IEnumerable<Newsletter> Emails { get; set; }
        public Pager Pager { get; set; }
    }
}