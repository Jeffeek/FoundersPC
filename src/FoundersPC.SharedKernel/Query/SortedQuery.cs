using System;
using System.Linq;
using FoundersPC.SharedKernel.Extensions;

namespace FoundersPC.SharedKernel.Query
{
    public abstract class SortedQuery<TSource> : Query<TSource>, ISortedQuery<TSource>
    {
        public string SortColumn { get; set; }

        public bool IsAscending { get; set; }

        protected SortedQuery()
        {
            SortColumn = "Id";
            IsAscending = false;
        }

        public virtual Func<IQueryable<TSource>, IOrderedQueryable<TSource>> GetSortingExpression() =>
            SortColumn switch
            {
                _ => item => item.ApplyOrder(SortColumn, IsAscending)
            };
    }
}