using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Blogifier.Shared
{
    public class Post : IDisposable
    {
        public Post() { }

        public int Id { get; set; }
        public int AuthorId { get; set; }

        public PostType PostType { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        [Required]
        [StringLength(160)]
        public string Slug { get; set; }
        [Required]
        [StringLength(450)]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [StringLength(160)]
        public string Cover { get; set; }
        public int PostViews { get; set; }
        public double Rating { get; set; }
        public bool IsFeatured { get; set; }
        public bool Selected { get; set; }

        public DateTime Published { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Blog Blog { get; set; }
        public List<PostCategory> PostCategories { get; set; }
        public List<Comment> Comments { get; set; }
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
}
