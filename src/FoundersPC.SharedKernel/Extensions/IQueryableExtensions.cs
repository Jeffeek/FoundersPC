﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.SharedKernel.Query;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.SharedKernel.Extensions
{
    public static class QueryableExtensions
    {
        public static IPagedList<T> ApplyPaging<T>(this IQueryable<T> items, int pageNumber, int pageSize) =>
            new PagedList<T>(items, pageNumber, pageSize);

        public static IPagedList<T> ApplyPaging<T, TPagedFilter>(this IQueryable<T> items, TPagedFilter filter)
            where TPagedFilter : IPagedFilter =>
            new PagedList<T>(items, filter.PageNumber, filter.PageSize);

        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> items, IQuery<T> query) where T : class
        {
            var result = items.Where(query.GetExpression());

            result = query.GetIncludes()
                          .Aggregate(result, (current, include) => current.Include(include));

            return result;
        }

        public static IOrderedQueryable<T> ApplyOrder<T, TType>(this IQueryable<T> @this, Expression<Func<T, TType>> keySelector, bool isAscending) =>
            isAscending ? @this.OrderBy(keySelector) : @this.OrderByDescending(keySelector);

        private static readonly MethodInfo OrderBy = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                                      .First(x => x.Name == "OrderBy"
                                                                                  && x.GetParameters()
                                                                                      .Length
                                                                                  == 2);

        private static readonly MethodInfo OrderByDescending = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                                                .First(x => x.Name == "OrderByDescending"
                                                                                            && x.GetParameters()
                                                                                                .Length
                                                                                            == 2);

        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> @this, string sortColumn, bool isAscending)
        {
            var orderProperty = typeof(T).GetProperties()
                                         .FirstOrDefault(x => x.Name.Equals(sortColumn, StringComparison.InvariantCultureIgnoreCase))
                                ?? typeof(T).GetProperties()
                                            .First(x => x.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase));

            var selectorParam = Expression.Parameter(typeof(T), "keySelector");

            return (IOrderedQueryable<T>)@this.Provider.CreateQuery<T>(Expression.Call((!isAscending ? OrderByDescending : OrderBy)
                                                                                       .MakeGenericMethod(typeof(T),
                                                                                                              orderProperty.PropertyType),
                                                                                       @this.Expression,
                                                                                       Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T),
                                                                                                             orderProperty.PropertyType),
                                                                                                         Expression.Property(selectorParam, orderProperty),
                                                                                                         selectorParam)));
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : IIdentityItem<int>
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return source.OrderBy(x => x.Id)
                         .Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize);
        }
    }
}