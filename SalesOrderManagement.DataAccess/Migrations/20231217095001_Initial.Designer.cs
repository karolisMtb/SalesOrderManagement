﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesManagementApp.DataAccess.DatabaseContext;

#nullable disable

namespace SalesOrderManagement.DataAccess.Migrations
{
    [DbContext(typeof(SalesManagementDb))]
    [Migration("20231217095001_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.SubElement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.Property<Guid?>("WindowId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WindowId");

                    b.ToTable("SubElements");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.Window", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuantityOfWindows")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Windows");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.SubElement", b =>
                {
                    b.HasOne("SalesManagementApp.DataAccess.Entities.Window", null)
                        .WithMany("SubElements")
                        .HasForeignKey("WindowId");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.Window", b =>
                {
                    b.HasOne("SalesManagementApp.DataAccess.Entities.Order", null)
                        .WithMany("Windows")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.Order", b =>
                {
                    b.Navigation("Windows");
                });

            modelBuilder.Entity("SalesManagementApp.DataAccess.Entities.Window", b =>
                {
                    b.Navigation("SubElements");
                });
#pragma warning restore 612, 618
        }
    }
}
