using Blogifier.Identity;
using Blogifier.Options;
using Blogifier.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Data;

public class AppDbContext : IdentityUserContext<UserInfo, string>
{
  protected readonly DbContextOptions<AppDbContext> _options;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    _options = options;
  }

  public DbSet<OptionInfo> Options { get; set; }
  public DbSet<Blog> Blogs { get; set; }
  public DbSet<Post> Posts { get; set; }
  public DbSet<Storage> Storages { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Subscriber> Subscribers { get; set; }
  public DbSet<Newsletter> Newsletters { get; set; }
  public DbSet<MailSetting> MailSettings { get; set; }
  public DbSet<PostCategory> PostCategories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<UserInfo>(e =>
    {
      e.ToTable("User");
      e.Property(p => p.Id).HasMaxLength(128);
      e.Property(p => p.CreatedAt).HasColumnOrder(0);
      e.Property(p => p.PasswordHash).HasMaxLength(256);
      e.Property(p => p.SecurityStamp).HasMaxLength(32);
      e.Property(p => p.ConcurrencyStamp).HasMaxLength(64);
      e.Property(p => p.PhoneNumber).HasMaxLength(32);
    });

    modelBuilder.Entity<IdentityUserClaim<string>>(e =>
    {
      e.ToTable("UserClaim");
      e.Property(p => p.ClaimType).HasMaxLength(16);
      e.Property(p => p.ClaimValue).HasMaxLength(256);
    });
    modelBuilder.Entity<IdentityUserLogin<string>>(e =>
    {
      e.ToTable("UserLogin");
      e.Property(p => p.ProviderDisplayName).HasMaxLength(128);
    });
    modelBuilder.Entity<IdentityUserToken<string>>(e =>
    {
      e.ToTable("UserToken");
      e.Property(p => p.Value).HasMaxLength(1024);
    });

    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.ToTable("Options");
      e.HasIndex(b => b.Key).IsUnique();
    });

    modelBuilder.Entity<PostCategory>(e =>
    {
      e.ToTable("PostCategories");
      e.HasKey(t => new { t.PostId, t.CategoryId });
      e.HasOne(pt => pt.Post)
       .WithMany(p => p.PostCategories)
       .HasForeignKey(pt => pt.PostId)
       .OnDelete(DeleteBehavior.Cascade);

      e.HasOne(pt => pt.Category)
       .WithMany(t => t.PostCategories)
       .HasForeignKey(pt => pt.CategoryId)
       .OnDelete(DeleteBehavior.Cascade);
    });
  }
}
