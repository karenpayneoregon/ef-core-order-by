using EntityFrameworkLibrary;
using EntityFrameworkLibrary.Models;
using Microsoft.EntityFrameworkCore;
using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;

namespace NorthWindExampleApp3.Classes;
internal class CustomerExamples
{

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

}
