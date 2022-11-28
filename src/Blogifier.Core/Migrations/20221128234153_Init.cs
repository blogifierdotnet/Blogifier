using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blogifier.Core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    Theme = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    IncludeFeatured = table.Column<int>(type: "int", nullable: false),
                    ItemsPerPage = table.Column<int>(type: "integer", nullable: false),
                    Cover = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    Logo = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    HeaderScript = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FooterScript = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    AnalyticsListType = table.Column<int>(type: "integer", nullable: false),
                    AnalyticsPeriod = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Password = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Bio = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    VerificationToken = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Verified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    BlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Host = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    UserEmail = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    UserPassword = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    FromName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    FromEmail = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    ToName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    BlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailSettings_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Ip = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Country = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Region = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    BlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    PostType = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Slug = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Cover = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    PostViews = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    Selected = table.Column<bool>(type: "boolean", nullable: false),
                    Published = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    BlogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    Success = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
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
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_Authors_BlogId",
                table: "Authors",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_MailSettings_BlogId",
                table: "MailSettings",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Newsletters_PostId",
                table: "Newsletters",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategories_CategoryId",
                table: "PostCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_BlogId",
                table: "Subscribers",
                column: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailSettings");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
