using NorthWindExampleApp.Data;
using EntityFrameworkLibrary;
using Microsoft.EntityFrameworkCore;
using NorthWindExampleApp.Models;

namespace NorthWindExampleApp.Classes;
public class ContactsExamples
{
    /// <summary>
    /// Here we are ordering by navigation contact type, contact title
    /// </summary>
    public static async Task<List<Contacts>> OrderByContactTitleAscending()
    {
        await using var context = new Context();

        return await context
            .Contacts
            .Include(c => c.ContactTypeIdentifierNavigation)
            .OrderByColumn("ContactTypeIdentifierNavigation.ContactTitle")
            .ToListAsync();
    }
}
