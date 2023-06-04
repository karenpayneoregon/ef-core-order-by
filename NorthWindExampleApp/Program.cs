using EntityFrameworkLibrary.Models;
using NorthWindExampleApp.Classes;
using NorthWindExampleApp.Data;
using NorthWindExampleApp.Models;

namespace NorthWindExampleApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        await ProductsExamples.InefficientDemo("ProductName");
        await ProductsExamples.EfficientDemo("ProductName");

        List<Contacts> contacts = await ContactsExamples.OrderByContactTitleAscending();
        contacts.ToJson("contacts");

        List<Products> productsByCategoryAscending = await ProductsExamples.OrderByNavigationCategories();
        productsByCategoryAscending.ToJson("productsByCategoryAscending");

        List<Customers> customersRandom = await CustomersOrderByRandomColumnNameAscending();
        customersRandom.ToJson("CustomersOrderByRandomColumnNameAscending");

        List<Customers> customers1 = await CustomersOrderByContactTitleConventionalDescending();
        customers1.ToJson("CustomersOrderByContactTitleConventionalDescending");

        List<Customers> customers2 = await CustomersOrderByContactTitleDescending1();
        customers2.ToJson("CustomersOrderByContactTitleDescending1");

        List<Customers> customers3 = await CustomersOrderByContactTitleDescending2();

        customers3.ToJson("CustomersOrderByContactTitleDescending2");

        ExitPrompt();
    }

    // conventional order by descending 
    static async Task<List<Customers>> CustomersOrderByContactTitleConventionalDescending() 
        => await CustomerExamples.ConventionalOrderByOnNavigation();

    // order by descending using generic extension method passing strings
    static async Task<List<Customers>> CustomersOrderByContactTitleDescending1()
    {
        var ordering = "ContactTypeIdentifierNavigation.ContactTitle";
        return await CustomerExamples.OrderByOnNavigation1(ordering, OrderingDirection.Descending);
    }

    // order by a random column name
    static async Task<List<Customers>> CustomersOrderByRandomColumnNameAscending()
    {
        return await CustomerExamples.OrderByOnRandom();
    }

    // order by descending using generic extension method using strong typed 
    static async Task<List<Customers>> CustomersOrderByContactTitleDescending2()
    {
        var ordering = CustomersOrderColumns.List().FirstOrDefault(o => o.PropertyName == CustomerPropertyName.Title);
        return await CustomerExamples.OrderByOnNavigation2(ordering, OrderingDirection.Descending);
    }
}
