using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data
{
	public class BlogifierDbContext : DbContext
    {
        public BlogifierDbContext(DbContextOptions<BlogifierDbContext> options) : base(options) { }

        #region DB Sets

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<CustomField> CustomFields { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; } 

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ApplicationSettings.DatabaseOptions(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscriber>().HasIndex(s => s.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
