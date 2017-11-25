using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blogifier.Core.Data.Migrations
{
    public partial class InitBlogifierDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 450, nullable: true),
                    ImgSrc = table.Column<string>(maxLength: 160, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(maxLength: 160, nullable: false),
                    Title = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomKey = table.Column<string>(maxLength: 140, nullable: false),
                    CustomType = table.Column<int>(nullable: false),
                    CustomValue = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorEmail = table.Column<string>(maxLength: 160, nullable: false),
                    AuthorName = table.Column<string>(maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(maxLength: 160, nullable: true),
                    BlogTheme = table.Column<string>(maxLength: 160, nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: false),
                    IdentityName = table.Column<string>(maxLength: 100, nullable: false),
                    Image = table.Column<string>(maxLength: 160, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Logo = table.Column<string>(maxLength: 160, nullable: true),
                    Slug = table.Column<string>(maxLength: 160, nullable: false),
                    Title = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetType = table.Column<int>(nullable: false),
                    DownloadCount = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    Length = table.Column<long>(nullable: false),
                    Path = table.Column<string>(maxLength: 250, nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 160, nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: false),
                    Image = table.Column<string>(maxLength: 160, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    PostViews = table.Column<int>(nullable: false),
                    ProfileId = table.Column<int>(nullable: false),
                    Published = table.Column<DateTime>(nullable: false),
                    Slug = table.Column<string>(maxLength: 160, nullable: false),
                    Title = table.Column<string>(maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true)
                        .Annotation("MySql:ValueGeneratedOnAdd", true)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlogPostId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCategories_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProfileId",
                table: "Assets",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ProfileId",
                table: "BlogPosts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_BlogPostId",
                table: "PostCategories",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "CustomFields");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
