using System;
using System.Collections.Generic;

namespace Blogifier.Models
{
    public class NewsletterModel
    {
        public IEnumerable<Newsletter> Emails { get; set; }
        public Pager Pager { get; set; }
    }

    public class Newsletter
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public DateTime Created { get; set; }
    }
}
