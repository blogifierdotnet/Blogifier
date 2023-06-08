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
        public DbSet<PostCategory> PostCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>()
                .HasKey(t => new { t.PostId, t.CategoryId });

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(t => t.PostCategories)
                .HasForeignKey(pt => pt.CategoryId);

            string sql = "getdate()";

            if (_options.Extensions != null)
            {
                foreach (var ext in _options.Extensions)
                {
                    if (ext.GetType().ToString().StartsWith("Microsoft.EntityFrameworkCore.Sqlite"))
                    {
                        sql = "DATE('now')";
                        break;
                    }
                }
            }

            modelBuilder.Entity<Blog>().Property(b => b.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<Post>().Property(p => p.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<Author>().Property(a => a.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<Category>().Property(c => c.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<Subscriber>().Property(s => s.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<Newsletter>().Property(n => n.DateUpdated).HasDefaultValueSql(sql);
            modelBuilder.Entity<MailSetting>().Property(n => n.DateUpdated).HasDefaultValueSql(sql);
        }
    }
}
