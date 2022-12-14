// <auto-generated />
using System;
using DnDAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DnDAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221115205757_Mig01")]
    partial class Mig01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DnDAPI.Models.Entities.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CampaignDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CampaignName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DungeonMasterId")
                        .HasColumnType("int");

                    b.Property<int>("GameEdition")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DungeonMasterId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("DnDAPI.Models.Entities.DungeonMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("AdvancedDnD1E")
                        .HasColumnType("bit");

                    b.Property<bool?>("AdvancedDnD2E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons1E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons3E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons4E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons5E")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearsOfExperiance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DungeonMasters");
                });

            modelBuilder.Entity("DnDAPI.Models.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("AdvancedDnD1E")
                        .HasColumnType("bit");

                    b.Property<bool?>("AdvancedDnD2E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons1E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons3E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons4E")
                        .HasColumnType("bit");

                    b.Property<bool?>("DungeonAndDragons5E")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearsOfExperiance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DnDAPI.Models.Entities.Campaign", b =>
                {
                    b.HasOne("DnDAPI.Models.Entities.DungeonMaster", "DungeonMaster")
                        .WithMany("Campaigns")
                        .HasForeignKey("DungeonMasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DnDAPI.Models.Entities.Player", "Player")
                        .WithMany("Campaigns")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DungeonMaster");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DnDAPI.Models.Entities.DungeonMaster", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("DnDAPI.Models.Entities.Player", b =>
                {
                    b.Navigation("Campaigns");
                });
#pragma warning restore 612, 618
        }
    }
}
