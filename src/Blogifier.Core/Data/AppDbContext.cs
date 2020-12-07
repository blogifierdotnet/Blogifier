using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data
{
	public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
      { }

      public DbSet<Blog> Blogs { get; set; }
      public DbSet<Post> Posts { get; set; }
      public DbSet<Author> Authors { get; set; }
      public DbSet<Category> Categories { get; set; }
   }
}