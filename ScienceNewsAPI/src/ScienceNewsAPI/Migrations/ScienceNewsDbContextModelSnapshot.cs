using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ScienceNewsAPI.Data;

namespace ScienceNewsAPI.Migrations
{
    [DbContext(typeof(ScienceNewsDbContext))]
    partial class ScienceNewsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScienceNewsAPI.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract");

                    b.Property<string>("Link");

                    b.Property<DateTime>("PubDate");

                    b.Property<string>("ThumbnailUrl");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ThumbnailUrl");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ScienceNewsAPI.Models.Thumbnail", b =>
                {
                    b.Property<string>("Url")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Height");

                    b.Property<short>("Width");

                    b.HasKey("Url");

                    b.ToTable("Thumbnail");
                });

            modelBuilder.Entity("ScienceNewsAPI.Models.Item", b =>
                {
                    b.HasOne("ScienceNewsAPI.Models.Thumbnail", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailUrl");
                });
        }
    }
}
