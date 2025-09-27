using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;


namespace DatabaseBroker.Extensions;

public static class RepositoryExtensions
{
    public static async Task<(IEnumerable<T>, int total)> GetByTermsAsync<T>(this IQueryable<T> queryable,
        TermModelBase query)
    {
        var filtered = queryable
            .FilterByExpressions(query.FilteringExpressions);

        int total = await filtered.CountAsync();
        return (await filtered
            .Sort(query)
            .Paging(query)
            .ToListAsync(), total);
    }

    public static List<string> ExpressionsList = new() { "==", "!=", "<>", ">>", "<<", "<=", ">=", "$$" };

    /// <summary>
    ///     Filter by expressions
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="expressions">
    ///     an expression is [PropertyName][==, !=, <>, >>, <<, <=, >=]
    /// </param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IQueryable<T> FilterByExpressions<T>(this IQueryable<T> queryable, List<string>? expressions)
    {
        if (expressions is null || expressions.DefaultIfEmpty() == null)
            return queryable;

        foreach (var expressionStr in expressions)
        {
            var splitted = expressionStr.Split(ExpressionsList.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var expressionStrType = expressionStr.Substring(splitted[0].Length, 2);

            if (splitted.Length < 2 || !ExpressionsList.Contains(expressionStrType))
                continue;

            var filterData = new { PropertyName = splitted[0], Value = splitted[1] };

            var property = typeof(T).GetProperties().FirstOrDefault(x =>
                x.Name.Equals(filterData.PropertyName, StringComparison.InvariantCultureIgnoreCase));

            if (property is not PropertyInfo)
                throw new Exception($"{filterData.PropertyName} named property not found");


            var parameter = Expression.Parameter(typeof(T), "x");

            var ilikeMethod = typeof(NpgsqlDbFunctionsExtensions)
                .GetMethod("ILike", new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;

            var efFunctions = Expression.Constant(EF.Functions);
            
            object? rightActualTyped = filterData.Value;
            if (property.PropertyType.IsValueType)
            {
                if (string.IsNullOrEmpty(filterData.Value))
                    rightActualTyped = Activator.CreateInstance(property.PropertyType);
                else
                {
                    var nullableUnderlying = Nullable.GetUnderlyingType(property.PropertyType);
                    if (nullableUnderlying is not null)
                    {
                        rightActualTyped = Convert.ChangeType(filterData.Value, nullableUnderlying);
                    }
                    else
                    {
                        rightActualTyped = Convert.ChangeType(filterData.Value, property.PropertyType);
                    }
                }
            }

            var left = Expression.MakeMemberAccess(parameter, property);
            var right = Expression.Constant(rightActualTyped,  Nullable.GetUnderlyingType(property.PropertyType) is not null ? typeof(Nullable<>).MakeGenericType(rightActualTyped?.GetType()) : rightActualTyped?.GetType());

            Expression switchedExpression = expressionStrType switch
            {
                "!=" => Expression.NotEqual(left, right),
                "<>" => Expression.NotEqual(left, right),
                ">=" => Expression.GreaterThanOrEqual(left, right),
                "<=" => Expression.LessThanOrEqual(left, right),
                "<<" => Expression.LessThan(left, right),
                ">>" => Expression.GreaterThan(left, right),
                "==" => Expression.Equal(left, right),
                "$$" => Expression.Call(ilikeMethod, efFunctions, left, Expression.Constant($"%{filterData.Value}%")),
                _ => Expression.Equal(left, right)
            };


            var predicate = Expression
                .Lambda(
                    switchedExpression,
                    parameter
                );

            queryable = queryable
                .Where((Expression<Func<T, bool>>)predicate);
        }

        return queryable;
    }


    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, TermModelBase termModel)
    {
        var sortData = new { PropertyName = termModel.SortPropName, Direction = termModel.SortDirection };
        if (sortData.PropertyName is null)
            return queryable;
        var property = typeof(T).GetProperties().FirstOrDefault(x =>
            x.Name.Equals(sortData.PropertyName, StringComparison.InvariantCultureIgnoreCase));

        if (property is not PropertyInfo)
            throw new Exception($"{sortData.PropertyName} named property not found");

        var parameter = Expression.Parameter(typeof(T), "x");

        var lambda = (Expression<Func<T, object>>)Expression
            .Lambda(
                Expression.Convert(Expression.MakeMemberAccess(parameter, property), typeof(object)),
                // Expression.MakeMemberAccess(parameter, property),
                parameter
            );


        // if (sortData.Direction == "asc")
        // return queryable
        //     .OrderBy((Expression<Func<T, object>>)lambda);

        // Expression.Call(typeof(Queryable), "Sdf",  )
        // return Queryable.OrderByDescending<T, object>(queryable, lambda);

        return queryable.Provider
            .CreateQuery<T>(Expression.Call(
                typeof(Queryable),
                sortData.Direction == "asc" ? "OrderBy" : "OrderByDescending",
                new[] { queryable.ElementType, typeof(object) },
                queryable.Expression, lambda));
    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, TermModelBase termModel)
    {
        var pagingData = new { termModel.Skip, termModel.Take };
        if (pagingData.Skip == -1 && pagingData.Take == -1)
            return queryable;

        return queryable
            .Skip(pagingData.Skip)
            .Take(pagingData.Take);
    }

    public static IQueryable<TOutput> LeftJoin2<TLeft, TRight, TKey, TOutput>(
        this IQueryable<TLeft> left,
        IEnumerable<TRight> right,
        Expression<Func<TLeft, TKey>> leftKey,
        Expression<Func<TRight, TKey>> rightKey,
        Expression<Func<TLeft, TRight?, TOutput>> join)
    {
        var paramJ = Expression.Parameter(typeof(LeftJoinInternal<TLeft, TRight>));
        var paramR = Expression.Parameter(typeof(TRight));
        var body = Expression.Invoke(join, Expression.Field(paramJ, "L"), paramR);
        var l = Expression.Lambda<Func<LeftJoinInternal<TLeft, TRight>, TRight, TOutput>>(body, paramJ, paramR);

        return left
            .GroupJoin(right, leftKey, rightKey, (l, r) => new LeftJoinInternal<TLeft, TRight> { L = l, R = r })
            .SelectMany(j => j.R.DefaultIfEmpty()!, l);
    }

    private sealed class LeftJoinInternal<TLeft, TRight>
    {
        public TLeft L = default!;
        public IEnumerable<TRight> R = default!;
    }
}