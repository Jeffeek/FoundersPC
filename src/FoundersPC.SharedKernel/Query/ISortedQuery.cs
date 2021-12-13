#region Using namespaces

using System;
using System.Linq;

#endregion

namespace FoundersPC.SharedKernel.Query;

public interface ISortedQuery<TSource> : IQuery<TSource>
{
    Func<IQueryable<TSource>, IOrderedQueryable<TSource>> GetSortingExpression();
}