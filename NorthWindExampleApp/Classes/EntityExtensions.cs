using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NorthWindExampleApp.Models;

namespace NorthWindExampleApp.Classes;

public static class EntityExtensions
{
    /// <summary>
    /// Get each type of model in a <see cref="DbContext"/>
    /// </summary>
    /// <param name="context">active DbContext</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static List<Type> GetModelNames(this DbContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        return context.Model.GetEntityTypes().Select(et => et.ClrType).ToList();
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