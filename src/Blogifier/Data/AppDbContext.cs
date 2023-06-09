using Blogifier.Identity;
using Blogifier.Newsletters;
using Blogifier.Options;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Data;

public class AppDbContext : IdentityUserContext<UserInfo, int>
{
  public AppDbContext(DbContextOptions options) : base(options)
  {

  }
  public DbSet<OptionInfo> Options { get; set; } = default!;
  public DbSet<Post> Posts { get; set; } = default!;
  public DbSet<Category> Categories { get; set; } = default!;
  public DbSet<PostCategory> PostCategories { get; set; } = default!;
  public DbSet<Newsletter> Newsletters { get; set; } = default!;
  public DbSet<Subscriber> Subscribers { get; set; } = default!;
  public DbSet<Storage> Storages { get; set; } = default!;
  //public DbSet<StorageReference> StorageReferences { get; set; } = default!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<UserInfo>(e =>
    {
      e.ToTable("Users");
      e.Property(p => p.Id).HasMaxLength(128);
      e.Property(p => p.CreatedAt).HasColumnOrder(0);
      e.Property(p => p.UpdatedAt).HasColumnOrder(1);
      e.Property(p => p.PasswordHash).HasMaxLength(256);
      e.Property(p => p.SecurityStamp).HasMaxLength(32);
      e.Property(p => p.ConcurrencyStamp).HasMaxLength(64);
      e.Property(p => p.PhoneNumber).HasMaxLength(32);
    });

    modelBuilder.Entity<IdentityUserClaim<int>>(e =>
    {
      e.ToTable("UserClaim");
      e.Property(p => p.ClaimType).HasMaxLength(16);
      e.Property(p => p.ClaimValue).HasMaxLength(256);
    });
    modelBuilder.Entity<IdentityUserLogin<int>>(e =>
    {
      e.ToTable("UserLogin");
      e.Property(p => p.ProviderDisplayName).HasMaxLength(128);
    });
    modelBuilder.Entity<IdentityUserToken<int>>(e =>
    {
      e.ToTable("UserToken");
      e.Property(p => p.Value).HasMaxLength(1024);
    });

    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.ToTable("Options");
      e.HasIndex(b => b.Key).IsUnique();
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.ToTable("Posts");
      e.HasIndex(b => b.Slug).IsUnique();
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.ToTable("Storages");
    });

    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //  e.ToTable("StorageReferences");
    //  e.HasKey(t => new { t.StorageId, t.EntityId });
    //});

    modelBuilder.Entity<PostCategory>(e =>
    {
      e.ToTable("PostCategories");
      e.HasKey(t => new { t.PostId, t.CategoryId });
    });
  }
}
