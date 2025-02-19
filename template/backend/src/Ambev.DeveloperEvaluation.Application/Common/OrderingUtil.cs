using Ambev.DeveloperEvaluation.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Common;

public static class OrderingUtil<T> where T : BaseEntity
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
                orderedQuery = descending ? orderedQuery.OrderByDescending(GetPropertyExpression<T>(property))
                                          : orderedQuery.OrderBy(GetPropertyExpression<T>(property));
                firstOrder = false;
            }
            else
            {
                orderedQuery = descending ? ((IOrderedQueryable<T>)orderedQuery).ThenByDescending(GetPropertyExpression<T>(property))
                                          : ((IOrderedQueryable<T>)orderedQuery).ThenBy(GetPropertyExpression<T>(property));
            }
        }

        return orderedQuery;
    }

    private static Expression<Func<T, object>> GetPropertyExpression<T>(string propertyName)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(param, propertyName);
        var converted = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, param);
    }
}