using Microsoft.Data.SqlClient;
using System.Data;

namespace SqlServerLibrary;

public class DataHelpers
{
    /// <summary>
    /// Checks if a LocalDB database with the specified name exists.
    /// </summary>
    /// <param name="databaseName">The name of the database to check for existence.</param>
    /// <returns>
    /// <see langword="true"/> if the database exists; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool LocalDbDatabaseExists(string databaseName)
    {
        using var cn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;integrated security=True;Encrypt=False");
        using var cmd = new SqlCommand($"SELECT DB_ID('{databaseName}'); ", cn);

        cn.Open();
        return cmd.ExecuteScalar() != DBNull.Value;

    }
}