﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;
using System;
using System.Collections.Generic;

namespace NorthWindExampleApp.Data.Configurations
{
    public partial class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> entity)
        {
            entity.HasKey(e => e.ProductID);

            entity.HasIndex(e => e.CategoryID, "IX_Products_CategoryID");

            entity.HasIndex(e => e.SupplierID, "IX_Products_SupplierID");

            entity.Property(e => e.ProductName)
            .IsRequired()
            .HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryID)
            .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
            .HasForeignKey(d => d.SupplierID)
            .HasConstraintName("FK_Products_Suppliers");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Products> entity);
    }
}
