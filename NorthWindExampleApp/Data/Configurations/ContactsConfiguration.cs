﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;
using System;
using System.Collections.Generic;

namespace NorthWindExampleApp.Data.Configurations
{
    public partial class ContactsConfiguration : IEntityTypeConfiguration<Contacts>
    {
        public void Configure(EntityTypeBuilder<Contacts> entity)
        {
            entity.HasKey(e => e.ContactId);

            entity.HasIndex(e => e.ContactTypeIdentifier, "IX_Contacts_ContactTypeIdentifier");

            entity.Property(e => e.FullName).HasComputedColumnSql("(([FirstName]+' ')+[LastName])", false);

            entity.HasOne(d => d.ContactTypeIdentifierNavigation).WithMany(p => p.Contacts)
            .HasForeignKey(d => d.ContactTypeIdentifier)
            .HasConstraintName("FK_Contacts_ContactType");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Contacts> entity);
    }
}