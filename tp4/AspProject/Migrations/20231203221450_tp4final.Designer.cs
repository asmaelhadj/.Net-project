﻿// <auto-generated />
using System;
using AspProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AspProject.Migrations
{
    [DbContext(typeof(ApplicationdbContext))]
    [Migration("20231203221450_tp4final")]
    partial class tp4final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AspProject.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MembershiptypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MembershiptypeId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("AspProject.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("84ca0bcd-082c-49cb-aa77-ea2f1f5f8285"),
                            Name = "GenreFromJsonFile1"
                        },
                        new
                        {
                            Id = new Guid("79e6f638-d7e7-4f63-8365-f172cb925381"),
                            Name = "GenreFromJsonFile2"
                        });
                });

            modelBuilder.Entity("AspProject.Models.MemberShipType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DicountRate")
                        .HasColumnType("float");

                    b.Property<int>("DurationInMonth")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SignUpFee")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("memberShipTypes");
                });

            modelBuilder.Entity("AspProject.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("CustomerMovie", b =>
                {
                    b.Property<Guid>("CustomersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomersId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CustomerMovie");
                });

            modelBuilder.Entity("AspProject.Models.Customer", b =>
                {
                    b.HasOne("AspProject.Models.MemberShipType", "Membershiptype")
                        .WithMany("Customers")
                        .HasForeignKey("MembershiptypeId");

                    b.Navigation("Membershiptype");
                });

            modelBuilder.Entity("AspProject.Models.Movie", b =>
                {
                    b.HasOne("AspProject.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("CustomerMovie", b =>
                {
                    b.HasOne("AspProject.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspProject.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AspProject.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("AspProject.Models.MemberShipType", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
