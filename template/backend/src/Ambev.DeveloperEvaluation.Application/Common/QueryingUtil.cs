using Ambev.DeveloperEvaluation.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Common;

public static class QueryingUtil<T> where T : BaseEntity
{
    public static IQueryable<T> ApplyOrdering(IQueryable<T> query, string order)
    {
        var orderParams = order.Split(",");
        var orderedQuery = query;
        bool firstOrder = true;

        foreach (var param in orderParams)
        {
            var orderSplit = param.Trim().Split(" ");
            var property = orderSplit[0];
            var descending = orderSplit.Length > 1 && orderSplit[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            if (firstOrder)
            {
                orderedQuery = descending ? orderedQuery.OrderByDescending(GetPropertyExpression(property))
                                          : orderedQuery.OrderBy(GetPropertyExpression(property));
                firstOrder = false;
            }
            else
            {
                orderedQuery = descending ? ((IOrderedQueryable<T>)orderedQuery).ThenByDescending(GetPropertyExpression(property))
                                          : ((IOrderedQueryable<T>)orderedQuery).ThenBy(GetPropertyExpression(property));
            }
        }

        return orderedQuery;
    }

    private static Expression<Func<T, object>> GetPropertyExpression(string propertyName)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(param, propertyName);
        var converted = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, param);
    }
}