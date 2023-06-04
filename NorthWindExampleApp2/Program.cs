using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using NorthWindExampleApp2.Data;
using NorthWindExampleApp2.Models;
using Spectre.Console;

namespace NorthWindExampleApp2;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        await using var context = new Context();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        AnsiConsole.MarkupLine("Products [white]order by[/] [cyan]category name[/] [white]then by[/] [cyan]product name[/]");
        /*
         * Order on navigation column
         */

        List<Product> products  = await context
            .Products
            .Include(p => p.Category)
            .OrderByColumn("Category.Name")
            .ThenByColumn("Name")
            .ToListAsync();

        foreach (var product in products)
        {
            Console.WriteLine($"{product.ProductId, -4}{product.Category.Name, -15}{product.Name}");
        }

        ExitPrompt();
    }
}
