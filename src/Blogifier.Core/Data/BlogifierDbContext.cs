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

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(ApplicationSettings.UseInMemoryDatabase)
                optionsBuilder.UseInMemoryDatabase();
            else
                optionsBuilder.UseSqlServer(ApplicationSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
