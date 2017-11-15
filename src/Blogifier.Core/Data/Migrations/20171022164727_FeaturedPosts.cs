using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blogifier.Core.Data.Migrations
{
    public partial class FeaturedPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Profiles",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "BlogPosts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "BlogPosts",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BlogPosts");
        }
    }
}
