﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NorthWindExampleApp4.Models;

public partial class ContactDevices
{
    public int id { get; set; }

    public int? ContactId { get; set; }

    public int? PhoneTypeIdentifier { get; set; }

    public string PhoneNumber { get; set; }

    public virtual Contacts Contact { get; set; }

    public virtual PhoneType PhoneTypeIdentifierNavigation { get; set; }
}