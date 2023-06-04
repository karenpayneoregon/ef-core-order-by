# About

Examples for EntityFrameworkLibrary.QueryableExtensions OrderBy extensions on SqlLite

One example, order by, then by

```csharp
List<Product> products  = await context
    .Products
    .Include(p => p.Category)
    .OrderByColumn("Category.Name")
    .ThenByColumn("Name")
    .ToListAsync();
```

