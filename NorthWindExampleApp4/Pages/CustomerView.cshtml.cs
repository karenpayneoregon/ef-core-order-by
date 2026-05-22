using EntityFrameworkLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthWindExampleApp4.Classes;
using NorthWindExampleApp4.Data;
using NorthWindExampleApp4.Models;


namespace NorthWindExampleApp4.Pages
{
    /// <summary>
    /// Represents the view model for managing and displaying customer data in the NorthWindExampleApp4 application.
    /// </summary>
    /// <remarks>
    /// This class is responsible for handling customer-related operations, including sorting, retrieving, 
    /// and displaying customer data. It interacts with the database context to fetch and manipulate data 
    /// and provides properties and methods to support Razor Pages functionality.
    /// </remarks>
    public class CustomerViewModel(Context context) : PageModel
    {
        public string NameSort { get; set; }
        public string CurrentSort { get; set; }
        public IList<Customers> Customers { get;set; } = null!;

        public SelectList ColumnList { get; set; }
        [BindProperty]
        public List<SqlColumn> SqlColumns { get; set; }

        [BindProperty]
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Shows how to do a sort via  asp-route and via select input from post event
        /// </summary>
        /// <param name="sortOrder">column name</param>
        public async Task OnGetAsync(string sortOrder)
        {
            if (context.Customers != null)
            {
                if (!string.IsNullOrWhiteSpace(sortOrder))
                {
                    Customers = await OrderByOnNavigation(sortOrder);
                }
                else
                {
                    Customers = await GetCustomers();
                }
                
                SqlColumns = context.GetModelProperties("Customers");
                ColumnList = new SelectList(SqlColumns, "Id", "Name");
            }

        }

        /// <summary>
        /// Handles the submission of a form to sort and display customers based on a selected column.
        /// </summary>
        /// <param name="id">
        /// The identifier of the selected column used for sorting.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This method retrieves the properties of the "Customers" model, determines whether the selected column 
        /// is a navigation property, and sorts the customers accordingly. It also updates the column list, 
        /// selected index, and JavaScript-related view data.
        /// </remarks>
        public async Task OnPostSubmit(int id)
        {
            SqlColumns = context.GetModelProperties("Customers");
            var current = SqlColumns.FirstOrDefault(x => x.Id == id);
            if (current!.IsNavigation)
            {
                Customers = await OrderByOnNavigation(current.NavigationValue);
            }
            else
            {
                Customers = await OrderByOnNavigation(current.Name);
            }

            ColumnList = new SelectList(SqlColumns, "Id", "Name");
            SelectedIndex = id;
            ViewData["JavaScript"] = id;
        }

        /// <summary>
        /// Retrieves a sorted list of customers based on the specified column name.
        /// </summary>
        /// <param name="ordering">
        /// The name of the column to sort by. This can include navigation properties.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a 
        /// list of <see cref="Customers"/> sorted by the specified column.
        /// </returns>
        /// <remarks>
        /// This method uses Entity Frameworks Include method to load related entities 
        /// and a custom extension method <see cref="EntityFrameworkLibrary.QueryableExtensions.OrderByColumn{T}"/> 
        /// to dynamically sort the data.
        /// </remarks>
        public async Task<List<Customers>> OrderByOnNavigation(string ordering)
        {

            return await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .OrderByColumn(ordering)
                .ToListAsync();

        }

        /// <summary>
        /// Retrieves a list of customers, including their related entities.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a 
        /// list of <see cref="Customers"/> with their related entities loaded.
        /// </returns>
        /// <remarks>
        /// This method uses Entity Framework's <c>Include</c> method to load related entities, 
        /// such as <see cref="Customers.CountryIdentifierNavigation"/>, 
        /// <see cref="Customers.Contact"/>, and 
        /// <see cref="Customers.ContactTypeIdentifierNavigation"/>.
        /// </remarks>
        public async Task<List<Customers>> GetCustomers() =>
            await context.Customers
                .Include(c => c.CountryIdentifierNavigation)
                .Include(c => c.Contact)
                .Include(c => c.ContactTypeIdentifierNavigation)
                .ToListAsync();
    }
}
