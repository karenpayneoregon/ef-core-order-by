using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NorthWindExampleApp.Data;
using NorthWindExampleApp3.Models;
#pragma warning disable CS8603

namespace NorthWindExampleApp3.Classes;

/// <summary>
/// Provides extension methods for working with entity models in the context of Entity Framework Core.
/// </summary>
/// <remarks>
/// This class contains utility methods to retrieve metadata and properties of entity models,
/// such as columns and navigation properties, to facilitate database operations.
/// </remarks>
public static class EntityExtensions
{

    /// <summary>
    /// Retrieves a list of columns for the "Customers" entity, including navigation properties.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="SqlColumn"/> objects representing the columns of the "Customers" entity.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Context"/> class to fetch the metadata for the "Customers" entity.
    /// It also adds specific navigation properties such as "FirstName", "LastName", and "Country".
    /// </remarks>
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
    /// Retrieves the properties of a specified entity model from the database context.
    /// </summary>
    /// <param name="context">The active <see cref="DbContext"/> instance used to access the database.</param>
    /// <param name="modelName">The name of the entity model whose properties are to be retrieved.</param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="SqlColumn"/> objects representing the properties of the specified entity model.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="context"/> parameter is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the specified <paramref name="modelName"/> does not correspond to a valid entity type in the context.
    /// </exception>
    /// <remarks>
    /// This method retrieves metadata about the properties of the specified entity model, including details such as
    /// whether a property is a primary key, foreign key, nullable, or its data type.
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
    /// <summary>
    /// Retrieves the CLR <see cref="Type"/> of an entity based on its model name within the specified <see cref="DbContext"/>.
    /// </summary>
    /// <param name="context">
    /// The <see cref="DbContext"/> instance containing the entity model.
    /// </param>
    /// <param name="modelName">
    /// The name of the entity model whose CLR <see cref="Type"/> is to be retrieved.
    /// </param>
    /// <returns>
    /// The CLR <see cref="Type"/> of the entity if found; otherwise, <see langword="null"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the <paramref name="context"/> is <see langword="null"/>.
    /// </exception>
    /// <remarks>
    /// This method searches the entity types defined in the <see cref="DbContext.Model"/> and matches them
    /// by their name to the provided <paramref name="modelName"/>.
    /// </remarks>
    private static Type GetEntityType(DbContext context, string modelName) =>
        context.Model.GetEntityTypes()
            .Select(eType => eType.ClrType)
            .FirstOrDefault(type => type.Name == modelName);
}