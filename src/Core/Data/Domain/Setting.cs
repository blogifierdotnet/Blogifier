using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class Setting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140)]
        public string SettingKey { get; set; }

        public string SettingValue { get; set; }
    }
}
