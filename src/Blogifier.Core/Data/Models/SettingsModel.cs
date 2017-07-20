using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogifier.Core.Data.Models
{
    public class SettingsProfile : AdminBaseModel
    {
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(160)]
        public string AuthorEmail { get; set; }
        [StringLength(160)]
        public string Avatar { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }

    public class SettingsPersonal
    {

    }

    public class SettingsCustom
    {

    }

    public class SettingsImport
    {

    }
}
