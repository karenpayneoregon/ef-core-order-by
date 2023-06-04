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
}
