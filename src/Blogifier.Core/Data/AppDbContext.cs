using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly DbContextOptions<AppDbContext> _options;

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            _options = options;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<MailSetting> MailSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}