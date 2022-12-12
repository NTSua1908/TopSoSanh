﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopSoSanh.Entity;

#nullable disable

namespace TopSoSanh.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20221212095627_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TopSoSanh.Entity.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("MaxPrice")
                        .HasColumnType("double");

                    b.Property<double>("MinPrice")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("TopSoSanh.Entity.PriceFluctuation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Hour")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("PriceFluctuations");
                });

            modelBuilder.Entity("TopSoSanh.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ItemUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TopSoSanh.Entity.Notification", b =>
                {
                    b.HasOne("TopSoSanh.Entity.Product", "Product")
                        .WithMany("Notifications")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TopSoSanh.Entity.PriceFluctuation", b =>
                {
                    b.HasOne("TopSoSanh.Entity.Product", "Product")
                        .WithMany("PriceFluctuations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TopSoSanh.Entity.Product", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("PriceFluctuations");
                });
#pragma warning restore 612, 618
        }
    }
}
