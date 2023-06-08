using Blogifier.Identity;
using Blogifier.Newsletters;
using Blogifier.Options;
using Blogifier.Shared;
using Blogifier.Storages;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Data;

public class MySqlDbContext : AppDbContext
{
  public MySqlDbContext(DbContextOptions options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


    modelBuilder.Entity<UserInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });
    modelBuilder.Entity<OptionInfo>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Post>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Category>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    modelBuilder.Entity<Newsletter>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Subscriber>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
      e.Property(b => b.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    });

    modelBuilder.Entity<Storage>(e =>
    {
      e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    });

    //modelBuilder.Entity<StorageReference>(e =>
    //{
    //  e.Property(b => b.CreatedAt).ValueGeneratedOnAdd();
    //});
  }
}
