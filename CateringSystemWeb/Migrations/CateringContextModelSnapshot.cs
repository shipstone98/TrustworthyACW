﻿// <auto-generated />
using System;
using CateringSystemWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CateringSystemWeb.Migrations
{
    [DbContext(typeof(CateringContext))]
    partial class CateringContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CateringSystem.CartItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PayPalOrderUnitID")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PayPalOrderUnitID");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("CateringSystem.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrder", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproveLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UnitID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UnitID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrderLink", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HyperTextReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<string>("PayPalOrderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("PayPalOrderID");

                    b.ToTable("PayPalOrderLink");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrderUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Intent")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PayPalOrderUnit");
                });

            modelBuilder.Entity("CateringSystem.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("nvarchar(63)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CateringSystem.CartItem", b =>
                {
                    b.HasOne("CateringSystem.PayPal.PayPalOrderUnit", null)
                        .WithMany("Items")
                        .HasForeignKey("PayPalOrderUnitID");

                    b.HasOne("CateringSystem.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrder", b =>
                {
                    b.HasOne("CateringSystem.PayPal.PayPalOrderUnit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitID");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrderLink", b =>
                {
                    b.HasOne("CateringSystem.PayPal.PayPalOrder", null)
                        .WithMany("Links")
                        .HasForeignKey("PayPalOrderID");
                });

            modelBuilder.Entity("CateringSystem.Product", b =>
                {
                    b.HasOne("CateringSystem.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CateringSystem.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrder", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("CateringSystem.PayPal.PayPalOrderUnit", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
