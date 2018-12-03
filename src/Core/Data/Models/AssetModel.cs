using Core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Data
{
    public class AssetsModel
    {
        public Pager Pager { get; set; }
        public IEnumerable<AssetItem> Assets { get; set; }
    }

    public class AssetItem
    {
        public AssetType AssetType {
            get {
                return Path.IsImagePath() ? AssetType.Image : AssetType.Attachment;
            }
        }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        [Required]
        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
    }

    public enum AssetType
    {
        Image = 0,
        Attachment = 1
    }
}