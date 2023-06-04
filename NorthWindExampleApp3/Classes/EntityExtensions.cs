﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NorthWindExampleApp.Data;
using NorthWindExampleApp3.Models;
#pragma warning disable CS8603

namespace NorthWindExampleApp3.Classes;

public static class EntityExtensions
{



    public static List<SqlColumn> GetCustomerColumns()
    {
        using var context = new Context();
        var list =  context.GetModelProperties("Customers");
        list.Add(new SqlColumn() {Name = "FirstName", IsNavigation = true, NavigationValue = "Contact.FirstName" });
        list.Add(new SqlColumn() {Name = "LastName", IsNavigation = true, NavigationValue = "Contact.LastName" });
        list.Add(new SqlColumn() {Name = "Country", IsNavigation = true, NavigationValue = "CountryIdentifierNavigation.Name" });

        return list;
    }
    /// <summary>
    /// Get details for a model
    /// </summary>
    /// <param name="context">Active dbContext</param>
    /// <param name="modelName">Model name in context</param>
    /// <returns>List&lt;SqlColumn&gt;</returns>
    /// <remarks>
    /// More information can be added as needed
    /// </remarks>
    public static List<SqlColumn> GetModelProperties(this DbContext context, string modelName)
    {

        if (context == null) throw new ArgumentNullException(nameof(context));

        var entityType = GetEntityType(context, modelName);

        var list = new List<SqlColumn>();

        IEnumerable<IProperty> properties = context.Model
            .FindEntityType(entityType ?? throw new InvalidOperationException())!
            .GetProperties();

        foreach (IProperty itemProperty in properties)
        {
            SqlColumn sqlColumn = new()
            {
                Name = itemProperty.Name,
                IsPrimaryKey = itemProperty.IsKey(),
                IsForeignKey = itemProperty.IsForeignKey(),
                IsNullable = itemProperty.IsColumnNullable(),
                Type = itemProperty.PropertyInfo!.PropertyType
            };


            list.Add(sqlColumn);

        }

        return list;

    }
    private static Type GetEntityType(DbContext context, string modelName) =>
        context.Model.GetEntityTypes()
            .Select(eType => eType.ClrType)
            .FirstOrDefault(type => type.Name == modelName);
}