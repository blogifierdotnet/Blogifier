using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Data.Migrations.Sqlite
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Categories",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            Content = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
            Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Options",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            Key = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
            Value = table.Column<string>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Options", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Subscribers",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            Email = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
            Ip = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
            Country = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
            Region = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Subscribers", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "User",
          columns: table => new
          {
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            Id = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
            NickName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
            Avatar = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
            Bio = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
            Gender = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
            Type = table.Column<int>(type: "INTEGER", nullable: false),
            State = table.Column<int>(type: "INTEGER", nullable: false),
            UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
            NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
            Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
            NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
            EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
            PasswordHash = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
            SecurityStamp = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
            ConcurrencyStamp = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
            PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
            TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
            LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
            LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
            AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_User", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Post",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            UserId = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
            Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
            Slug = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
            Description = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false),
            Content = table.Column<string>(type: "TEXT", nullable: false),
            Cover = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
            Views = table.Column<int>(type: "INTEGER", nullable: false),
            PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
            PostType = table.Column<int>(type: "INTEGER", nullable: false),
            State = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Post", x => x.Id);
            table.ForeignKey(
                      name: "FK_Post_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Storages",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            UserId = table.Column<string>(type: "TEXT", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
            DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
            Slug = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
            Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
            Path = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: false),
            Length = table.Column<long>(type: "INTEGER", nullable: false),
            ContentType = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
            Type = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Storages", x => x.Id);
            table.ForeignKey(
                      name: "FK_Storages_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserClaim",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            UserId = table.Column<string>(type: "TEXT", nullable: false),
            ClaimType = table.Column<string>(type: "TEXT", maxLength: 16, nullable: true),
            ClaimValue = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserClaim", x => x.Id);
            table.ForeignKey(
                      name: "FK_UserClaim_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserLogin",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
            ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
            ProviderDisplayName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
            UserId = table.Column<string>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
            table.ForeignKey(
                      name: "FK_UserLogin_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserToken",
          columns: table => new
          {
            UserId = table.Column<string>(type: "TEXT", nullable: false),
            LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
            Name = table.Column<string>(type: "TEXT", nullable: false),
            Value = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
            table.ForeignKey(
                      name: "FK_UserToken_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Newsletters",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()"),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            PostId = table.Column<int>(type: "INTEGER", nullable: false),
            Success = table.Column<bool>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Newsletters", x => x.Id);
            table.ForeignKey(
                      name: "FK_Newsletters_Post_PostId",
                      column: x => x.PostId,
                      principalTable: "Post",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "PostCategories",
          columns: table => new
          {
            PostId = table.Column<int>(type: "INTEGER", nullable: false),
            CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_PostCategories", x => new { x.PostId, x.CategoryId });
            table.ForeignKey(
                      name: "FK_PostCategories_Categories_CategoryId",
                      column: x => x.CategoryId,
                      principalTable: "Categories",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_PostCategories_Post_PostId",
                      column: x => x.PostId,
                      principalTable: "Post",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "StorageReferences",
          columns: table => new
          {
            StorageId = table.Column<int>(type: "INTEGER", nullable: false),
            EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            Type = table.Column<int>(type: "INTEGER", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_StorageReferences", x => new { x.StorageId, x.EntityId, x.Type });
            table.ForeignKey(
                      name: "FK_StorageReferences_Post_EntityId",
                      column: x => x.EntityId,
                      principalTable: "Post",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_StorageReferences_Storages_StorageId",
                      column: x => x.StorageId,
                      principalTable: "Storages",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Newsletters_PostId",
          table: "Newsletters",
          column: "PostId");

      migrationBuilder.CreateIndex(
          name: "IX_Options_Key",
          table: "Options",
          column: "Key",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Post_Slug",
          table: "Post",
          column: "Slug",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Post_UserId",
          table: "Post",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_PostCategories_CategoryId",
          table: "PostCategories",
          column: "CategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_StorageReferences_EntityId",
          table: "StorageReferences",
          column: "EntityId");

      migrationBuilder.CreateIndex(
          name: "IX_Storages_UserId",
          table: "Storages",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "EmailIndex",
          table: "User",
          column: "NormalizedEmail");

      migrationBuilder.CreateIndex(
          name: "UserNameIndex",
          table: "User",
          column: "NormalizedUserName",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_UserClaim_UserId",
          table: "UserClaim",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_UserLogin_UserId",
          table: "UserLogin",
          column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Newsletters");

      migrationBuilder.DropTable(
          name: "Options");

      migrationBuilder.DropTable(
          name: "PostCategories");

      migrationBuilder.DropTable(
          name: "StorageReferences");

      migrationBuilder.DropTable(
          name: "Subscribers");

      migrationBuilder.DropTable(
          name: "UserClaim");

      migrationBuilder.DropTable(
          name: "UserLogin");

      migrationBuilder.DropTable(
          name: "UserToken");

      migrationBuilder.DropTable(
          name: "Categories");

      migrationBuilder.DropTable(
          name: "Post");

      migrationBuilder.DropTable(
          name: "Storages");

      migrationBuilder.DropTable(
          name: "User");
    }
  }
}
