#region Using namespaces

using System;
using System.Linq;
using FoundersPC.SharedKernel.Extensions;

#endregion

namespace FoundersPC.SharedKernel.Query;

public abstract class SortedQuery<TSource> : Query<TSource>, ISortedQuery<TSource>
{
    protected SortedQuery()
    {
        SortColumn = "Id";
        IsAscending = false;
    }

    public string SortColumn { get; set; }

    public bool IsAscending { get; set; }

    public virtual Func<IQueryable<TSource>, IOrderedQueryable<TSource>> GetSortingExpression() =>
        SortColumn switch
        {
            _ => item => item.ApplyOrder(SortColumn, IsAscending)
        };
}