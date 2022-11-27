﻿// <auto-generated />
using System;
using Blogifier.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blogifier.Core.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220807065538_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Blogifier.Shared.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Bio")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int?>("BlogId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Blogifier.Shared.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnalyticsListType")
                        .HasColumnType("integer");

                    b.Property<int>("AnalyticsPeriod")
                        .HasColumnType("integer");

                    b.Property<string>("Cover")
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("character varying(450)");

                    b.Property<string>("FooterScript")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("HeaderScript")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int>("IncludeFeatured")
                        .HasColumnType("int");

                    b.Property<int>("ItemsPerPage")
                        .HasColumnType("integer");

                    b.Property<string>("Logo")
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Theme")
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Title")
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Blogifier.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Blogifier.Shared.MailSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("FromEmail")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("FromName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<string>("ToName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("MailSettings");
                });

            modelBuilder.Entity("Blogifier.Shared.Newsletter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("Blogifier.Shared.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int?>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cover")
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("character varying(450)");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("boolean");

                    b.Property<int>("PostType")
                        .HasColumnType("integer");

                    b.Property<int>("PostViews")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Published")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<bool>("Selected")
                        .HasColumnType("boolean");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blogifier.Shared.PostCategory", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("PostId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PostCategories");
                });

            modelBuilder.Entity("Blogifier.Shared.Subscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("Country")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("character varying(160)");

                    b.Property<string>("Ip")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("Region")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("Blogifier.Shared.Author", b =>
                {
                    b.HasOne("Blogifier.Shared.Blog", null)
                        .WithMany("Authors")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("Blogifier.Shared.MailSetting", b =>
                {
                    b.HasOne("Blogifier.Shared.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("Blogifier.Shared.Newsletter", b =>
                {
                    b.HasOne("Blogifier.Shared.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Blogifier.Shared.Post", b =>
                {
                    b.HasOne("Blogifier.Shared.Author", null)
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blogifier.Shared.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("Blogifier.Shared.PostCategory", b =>
                {
                    b.HasOne("Blogifier.Shared.Category", "Category")
                        .WithMany("PostCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blogifier.Shared.Post", "Post")
                        .WithMany("PostCategories")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Blogifier.Shared.Subscriber", b =>
                {
                    b.HasOne("Blogifier.Shared.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("Blogifier.Shared.Author", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Blogifier.Shared.Blog", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Blogifier.Shared.Category", b =>
                {
                    b.Navigation("PostCategories");
                });

            modelBuilder.Entity("Blogifier.Shared.Post", b =>
                {
                    b.Navigation("PostCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
