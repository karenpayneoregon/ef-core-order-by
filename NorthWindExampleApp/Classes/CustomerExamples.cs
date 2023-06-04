using EntityFrameworkLibrary;
using EntityFrameworkLibrary.Models;
using NorthWindExampleApp.Models;
using NorthWindExampleApp.Data;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace NorthWindExampleApp.Classes;
internal class CustomerExamples
{
    /// <summary>
    /// A standard order by
    /// </summary>
    public static async Task<List<Customers>> ConventionalOrderByOnNavigation()
    {
        await using var context = new Context();

        return await context.Customers
            .Include(c => c.CountryIdentifierNavigation)
            .Include(c => c.Contact)
            .Include(c => c.ContactTypeIdentifierNavigation)
            .OrderByDescending(c => c.ContactTypeIdentifierNavigation.ContactTitle)
            .ToListAsync();
    }
    /// <summary>
    /// Dynamic column top level ordering
    /// </summary>
    /// <param name="ordering"><see cref="OrderColumn"/>which column to order by</param>
    /// <param name="direction"><see cref="OrderingDirection"/>direction of order</param>
    public static async Task<List<Customers>> OrderByOnNavigation1(string ordering, OrderingDirection direction)
    {
        await using var context = new Context();

        if (direction == OrderingDirection.Ascending)
        {
            return await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .OrderByColumn(ordering)
                .ToListAsync();
        }
        else
        {
            return await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .OrderByColumnDescending(ordering)
                .ToListAsync();
        }

    }
    /// <summary>
    /// Random column order ascending
    /// </summary>
    public static async Task<List<Customers>> OrderByOnRandom()
    {
        await using var context = new Context();
        var ordering = context.GetModelProperties(nameof(Customers)).Shuffle().FirstOrDefault();
        
        AnsiConsole.MarkupLine($"[white]{nameof(OrderByOnRandom)}[/] on column [cyan]{ordering!.Name}[/]");

        return await context.Customers
            .Include(c => c.CountryIdentifierNavigation)
            .Include(c => c.Contact)
            .Include(c => c.ContactTypeIdentifierNavigation)
            .OrderByColumn(ordering!.Name)
            .ToListAsync();

    }

    /// <summary>
    /// Dynamic column at navigation level ordering
    /// </summary>
    /// <param name="ordering"><see cref="OrderColumn"/>which column to order by</param>
    /// <param name="direction"><see cref="OrderingDirection"/>direction of order</param>
    public static async Task<List<Customers>> OrderByOnNavigation2(OrderColumn ordering, OrderingDirection direction)
    {
        await using var context = new Context();

        if (direction == OrderingDirection.Ascending)
        {
            return await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .OrderByColumn(ordering.Column)
                .ToListAsync();
        }
        else
        {
            return await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .OrderByColumnDescending(ordering.Column)
                .ToListAsync();
        }

    }
}
