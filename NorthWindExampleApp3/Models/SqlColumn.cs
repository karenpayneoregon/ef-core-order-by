namespace NorthWindExampleApp3.Models;

public class SqlColumn
{
    public bool IsPrimaryKey { get; set; }
    public bool IsForeignKey { get; set; }
    public bool IsNullable { get; set; }
    public Type Type { get; set; }
    /// <summary>
    /// Column/property name
    /// </summary>
    public string Name { get; set; }

    public bool IsNavigation { get; set; }
    public string NavigationValue { get; set; }
    public override string ToString() => Name;

}