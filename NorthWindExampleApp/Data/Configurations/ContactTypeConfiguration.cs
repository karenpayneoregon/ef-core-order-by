﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;
using System;
using System.Collections.Generic;

namespace NorthWindExampleApp.Data.Configurations
{
    public partial class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> entity)
        {
            entity.HasKey(e => e.ContactTypeIdentifier);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ContactType> entity);
    }
}
