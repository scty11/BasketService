﻿// <auto-generated />
using System;
using BasketService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BasketService.Migrations
{
    [DbContext(typeof(BasketDbContext))]
    partial class BasketDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("BasketService.EntityModels.GiftVoucherEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Code");

                    b.HasKey("Id");

                    b.ToTable("GiftVouchers");
                });

            modelBuilder.Entity("BasketService.EntityModels.OfferVoucherEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<Guid?>("ProductTypeId");

                    b.Property<decimal>("ThresHold");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("OfferVouchers");
                });

            modelBuilder.Entity("BasketService.EntityModels.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BasketService.EntityModels.ProductTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("BasketService.EntityModels.OfferVoucherEntity", b =>
                {
                    b.HasOne("BasketService.EntityModels.ProductTypeEntity", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId");
                });

            modelBuilder.Entity("BasketService.EntityModels.ProductEntity", b =>
                {
                    b.HasOne("BasketService.EntityModels.ProductTypeEntity", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}