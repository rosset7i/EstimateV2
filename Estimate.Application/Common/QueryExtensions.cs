using System.Linq.Expressions;
using Estimate.Application.Common.Models.PagingAndSorting;
using Microsoft.EntityFrameworkCore;

namespace Estimate.Application.Common;

public static class QueryExtensions
{
    public static async Task<PagedResultOf<TOutput>> ToPagedListAsync<TOutput>(
        this IQueryable<TOutput> entities,
        PagedAndSortedRequest request)
    {
        var totalItems = await entities.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        var pagedAndSortedResult = await entities
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResultOf<TOutput>(
            totalPages,
            totalItems,
            request.Page,
            pagedAndSortedResult);
    }

    public static IQueryable<TEntity> SortBy<TEntity>(
        this IQueryable<TEntity> query,
        PagedAndSortedRequest request)
    {
        if (string.IsNullOrEmpty(request.SortBy))
            return query;

        var order = ConverterParaExpression<TEntity>(request.SortBy);

        if (string.Equals(request.Direction, SortDirection.Desc.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return query.OrderByDescending(order);

        return query.OrderBy(order);
    }

    private static Expression<Func<TEntity, object>> ConverterParaExpression<TEntity>(string orderBy)
    {
        var param = Expression.Parameter(typeof(TEntity), "e");

        UnaryExpression conversion;

        try
        {
            conversion = Expression.Convert(Expression.Property(param, orderBy), typeof(object));
        }
        catch (Exception)
        {
            conversion = Expression.Convert(Expression.Property(param, "Id"), typeof(object));
        }

        return Expression.Lambda<Func<TEntity, object>>(conversion, param);
    }

    public static IQueryable<TEntity> With<TEntity>(
        this IQueryable<TEntity> query,
        bool condition,
        Expression<Func<TEntity, bool>> func)
    {
        return condition ? query.Where(func) : query;
    }
}