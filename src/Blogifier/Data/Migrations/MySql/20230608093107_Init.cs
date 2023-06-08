using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogifier.Data.Migrations.MySql
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase()
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Categories",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            Content = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Options",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            Key = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Value = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Options", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Subscribers",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            Email = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Ip = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Country = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Region = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Subscribers", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "User",
          columns: table => new
          {
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            Id = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NickName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Avatar = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Bio = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Gender = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Type = table.Column<int>(type: "int", nullable: false),
            State = table.Column<int>(type: "int", nullable: false),
            UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            PasswordHash = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            SecurityStamp = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ConcurrencyStamp = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            PhoneNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
            TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
            LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
            LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
            AccessFailedCount = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_User", x => x.Id);
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Post",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            UserId = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Title = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Slug = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Description = table.Column<string>(type: "varchar(450)", maxLength: 450, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Content = table.Column<string>(type: "longtext", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Cover = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Views = table.Column<int>(type: "int", nullable: false),
            PublishedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            PostType = table.Column<int>(type: "int", nullable: false),
            State = table.Column<int>(type: "int", nullable: false)
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Storages",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UserId = table.Column<string>(type: "varchar(128)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
            DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
            Slug = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Path = table.Column<string>(type: "varchar(2048)", maxLength: 2048, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Length = table.Column<long>(type: "bigint", nullable: false),
            ContentType = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Type = table.Column<int>(type: "int", nullable: false)
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "UserClaim",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UserId = table.Column<string>(type: "varchar(128)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ClaimType = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ClaimValue = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "UserLogin",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            ProviderDisplayName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            UserId = table.Column<string>(type: "varchar(128)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4")
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "UserToken",
          columns: table => new
          {
            UserId = table.Column<string>(type: "varchar(128)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Name = table.Column<string>(type: "varchar(255)", nullable: false)
                  .Annotation("MySql:CharSet", "utf8mb4"),
            Value = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                  .Annotation("MySql:CharSet", "utf8mb4")
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "Newsletters",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
            PostId = table.Column<int>(type: "int", nullable: false),
            Success = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

      migrationBuilder.CreateTable(
          name: "PostCategories",
          columns: table => new
          {
            PostId = table.Column<int>(type: "int", nullable: false),
            CategoryId = table.Column<int>(type: "int", nullable: false)
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
          })
          .Annotation("MySql:CharSet", "utf8mb4");

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
          name: "Storages");

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
          name: "User");
    }
  }
}
