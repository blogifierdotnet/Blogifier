using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string UserId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Path { get; set; }

        [Required]
        [StringLength(250)]
        public string Url { get; set; }

        public long Length { get; set; }
        public int DownloadCount { get; set; }

        public AssetType AssetType { get; set; }
        public DateTime Published { get; set; }

        [NotMapped]
        public string Image
        {
            get
            {
                var ext = "blank.png";

                if (Url.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    ext = "xml.png";

                if (Url.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                    ext = "zip.png";

                if (Url.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    ext = "txt.png";

                if (Url.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    ext = "pdf.png";

                if (Url.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                    ext = "mp3.png";

                if (Url.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
                    ext = "mp4.png";

                if (Url.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ||
                    Url.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                    ext = "doc.png";

                if (Url.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                    Url.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    ext = "xls.png";

                return $"lib/img/doctypes/{ext}";
            }
        }
    }

    public enum AssetType
    {
        Image = 0,
        Attachment = 1
    }
}