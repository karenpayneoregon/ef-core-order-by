using System.Diagnostics;
using EntityFrameworkLibrary.Models;
using NorthWindExampleApp.Data;
using NorthWindExampleApp3.Classes;
using NorthWindExampleApp3.Models;

namespace NorthWindExampleApp3;

public partial class Form1 : Form
{
    BindingSource _bindingSource = new ();
    public Form1()
    {
        InitializeComponent();

        ColumnNamesComboBox.DataSource = EntityExtensions.GetCustomerColumns();
        DirectionComboBox.SelectedIndex = 0;

    }

    private async void PopulateButton_Click(object sender, EventArgs e)
    {
        SqlColumn current = (SqlColumn)ColumnNamesComboBox.SelectedItem;
        
        var column = current.IsNavigation ? current.NavigationValue : current.Name;

        _bindingSource.DataSource = await CustomerExamples
            .OrderByOnNavigation1(column, Enum.Parse<OrderingDirection>(DirectionComboBox.Text));
        
        dataGridView1.DataSource = _bindingSource;
        dataGridView1.Columns["Orders"]!.Visible = false;
        dataGridView1.ExpandColumns();
    }

}
