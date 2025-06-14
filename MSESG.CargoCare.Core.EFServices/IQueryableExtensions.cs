using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MSESG.CargoCare.Core.EFServices
{
    public static class QueryableExtensions
    {
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            // For .NET 6 EF Core, getting the actual SQL is more complex
            // This is a simplified approach that returns the query expression
            return query.ToString();
        }
    }
}