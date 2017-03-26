using Blogifier.Core.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data
{
	public class BlogifierDbContext : DbContext
    {
        public BlogifierDbContext(DbContextOptions<BlogifierDbContext> options) : base(options) { }

        #region DB Sets

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PublicationCategory> PublicationCategories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			//if (Settings.DbProvider == DbProvider.SqlServer)
			//{
			//    optionsBuilder.UseSqlServer(Settings.SqlServerConnectionString);
			//}
			//else if(Settings.DbProvider == DbProvider.PostgreSql)
			//{
			//    optionsBuilder.UseNpgsql(Settings.PostgreSqlConnectionString);
			//}
			//else
			//{
			//    optionsBuilder.UseInMemoryDatabase();
			//}
			optionsBuilder.UseInMemoryDatabase();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
