using System.Linq.Expressions;

namespace OrderLibrary;

/// <summary>
/// Extension methods to perform Order By using strings
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Orders the elements of a sequence based on the specified column path.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="columnPath">
    /// The dot-separated path of the column to order by. 
    /// For example, "PropertyName" or "Nested.PropertyName".
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the specified column.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="columnPath"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method uses reflection to dynamically construct an ordering expression
    /// based on the provided column path.
    /// </remarks>
    public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath) 
        => source.OrderByColumnUsing(columnPath, "OrderBy");

    /// <summary>
    /// Orders the elements of a sequence in descending order based on the specified column path.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="columnPath">
    /// The dot-separated path of the column to order by in descending order. 
    /// For example, "PropertyName" or "Nested.PropertyName".
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedQueryable{T}"/> whose elements are sorted in descending order according to the specified column.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="columnPath"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method uses reflection to dynamically construct an ordering expression
    /// based on the provided column path.
    /// </remarks>
    public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath) 
        => source.OrderByColumnUsing(columnPath, "OrderByDescending");

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence based on the specified column path.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="columnPath">
    /// The dot-separated path of the column to order by. 
    /// For example, "PropertyName" or "Nested.PropertyName".
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the specified column.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="columnPath"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method uses reflection to dynamically construct an ordering expression
    /// based on the provided column path.
    /// </remarks>
    public static IOrderedQueryable<T> ThenByColumn<T>(this IOrderedQueryable<T> source, string columnPath) 
        => source.OrderByColumnUsing(columnPath, "ThenBy");

    /// <summary>
    /// Performs a subsequent ordering of the elements in a sequence in descending order
    /// based on the specified column path.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="columnPath">
    /// The dot-separated path of the column to order by in descending order. 
    /// For example, "PropertyName" or "Nested.PropertyName".
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedQueryable{T}"/> whose elements are sorted in descending order
    /// according to the specified column.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="columnPath"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method uses reflection to dynamically construct an ordering expression
    /// based on the provided column path.
    /// </remarks>
    public static IOrderedQueryable<T> ThenByColumnDescending<T>(this IOrderedQueryable<T> source, string columnPath) 
        => source.OrderByColumnUsing(columnPath, "ThenByDescending");

    /// <summary>
    /// Orders the elements of a sequence based on the specified column path and ordering method.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence to order.</param>
    /// <param name="columnPath">
    /// The dot-separated path of the column to order by. 
    /// For example, "PropertyName" or "Nested.PropertyName".
    /// </param>
    /// <param name="method">
    /// The name of the ordering method to use, such as "OrderBy", "OrderByDescending", "ThenBy", or "ThenByDescending".
    /// </param>
    /// <returns>
    /// An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the specified column and method.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/>, <paramref name="columnPath"/>, or <paramref name="method"/> is <c>null</c>.
    /// </exception>
    /// <remarks>
    /// This method uses reflection to dynamically construct an ordering expression
    /// based on the provided column path and ordering method.
    /// </remarks>
    private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
    {
        var parameter = Expression.Parameter(typeof(T), "item");
        var member = columnPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        var keySelector = Expression.Lambda(member, parameter);
        var methodCall = Expression.Call(typeof(Queryable), method, [parameter.Type, member.Type],
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }
}
