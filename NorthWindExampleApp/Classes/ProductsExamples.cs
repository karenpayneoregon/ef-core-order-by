using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;
using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;

namespace NorthWindExampleApp.Classes;
public class ProductsExamples
{
    public static async Task<List<Products>> OrderByNavigationCategories()
    {
        await using var context = new Context();

        return await context.Products
            .Include(p => p.Category)
            .OrderByColumn("Category.CategoryName")
            .ToListAsync();
    }

    /// <summary>
    /// Worst
    /// </summary>
    /// <param name="columnName">name of column for order by</param>
    /// <returns></returns>
    public static async Task InefficientDemo(string columnName)
    {
        await using var context = new Context();
        var products = context.Products.ToList().OrderByPropertyName(columnName);
    }
    /// <summary>
    /// Best
    /// </summary>
    /// <param name="columnName">name of column for order by</param>
    /// <returns></returns>
    public static async Task EfficientDemo(string columnName)
    {
        await using var context = new Context();
        var products = context.Products.OrderByColumn(columnName).ToList();
    }
}
