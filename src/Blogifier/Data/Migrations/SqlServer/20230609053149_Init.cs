using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Blogifier.Data.Migrations.SqlServer
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
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            Content = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
            Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Options",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Options", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Subscribers",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            Email = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
            Ip = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
            Country = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
            Region = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Subscribers", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            Id = table.Column<int>(type: "int", maxLength: 128, nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            NickName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            Avatar = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
            Bio = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
            Gender = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
            Type = table.Column<int>(type: "int", nullable: false),
            State = table.Column<int>(type: "int", nullable: false),
            UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            SecurityStamp = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
            ConcurrencyStamp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
            PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
            PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            AccessFailedCount = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Posts",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            UserId = table.Column<int>(type: "int", nullable: false),
            Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
            Slug = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
            Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
            Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Cover = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: true),
            Views = table.Column<int>(type: "int", nullable: false),
            PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            PostType = table.Column<int>(type: "int", nullable: false),
            State = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Posts", x => x.Id);
            table.ForeignKey(
                      name: "FK_Posts_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Storages",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            UserId = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            Slug = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
            Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            Path = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
            Length = table.Column<long>(type: "bigint", nullable: false),
            ContentType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
            Type = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Storages", x => x.Id);
            table.ForeignKey(
                      name: "FK_Storages_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserClaim",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            UserId = table.Column<int>(type: "int", nullable: false),
            ClaimType = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
            ClaimValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserClaim", x => x.Id);
            table.ForeignKey(
                      name: "FK_UserClaim_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserLogin",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            ProviderDisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
            UserId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
            table.ForeignKey(
                      name: "FK_UserLogin_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserToken",
          columns: table => new
          {
            UserId = table.Column<int>(type: "int", nullable: false),
            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
            table.ForeignKey(
                      name: "FK_UserToken_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Newsletters",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            PostId = table.Column<int>(type: "int", nullable: false),
            Success = table.Column<bool>(type: "bit", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Newsletters", x => x.Id);
            table.ForeignKey(
                      name: "FK_Newsletters_Posts_PostId",
                      column: x => x.PostId,
                      principalTable: "Posts",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

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
                      name: "FK_PostCategories_Posts_PostId",
                      column: x => x.PostId,
                      principalTable: "Posts",
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
          name: "IX_PostCategories_CategoryId",
          table: "PostCategories",
          column: "CategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_Posts_Slug",
          table: "Posts",
          column: "Slug",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Posts_UserId",
          table: "Posts",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_Storages_UserId",
          table: "Storages",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_UserClaim_UserId",
          table: "UserClaim",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_UserLogin_UserId",
          table: "UserLogin",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "EmailIndex",
          table: "Users",
          column: "NormalizedEmail");

      migrationBuilder.CreateIndex(
          name: "UserNameIndex",
          table: "Users",
          column: "NormalizedUserName",
          unique: true,
          filter: "[NormalizedUserName] IS NOT NULL");
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
          name: "Posts");

      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
