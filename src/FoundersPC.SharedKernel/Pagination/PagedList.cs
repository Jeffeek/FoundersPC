using System.Collections.Generic;
using System.Linq;

namespace FoundersPC.SharedKernel.Pagination
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagingInfo PagingInfo { get; }

        public List<T> Result { get; }

        public PagedList(IQueryable<T> items, int pageNumber, int pageSize)
        {
            var totalItemCount = items?.Count() ?? 0;

            if (pageSize <= 0)
                pageSize = totalItemCount;

            if (pageNumber < 0)
                pageNumber = 0;

            PagingInfo = new PagingInfo(pageNumber, pageSize, totalItemCount);

            if (pageSize == totalItemCount)
                Result = items?.ToList() ?? new List<T>();
            else
                Result = items?.Skip(pageNumber * pageSize)
                              .Take(pageSize)
                              .ToList()
                         ?? new List<T>();
        }

        public PagedList(IEnumerable<T> items, PagingInfo info)
        {
            PagingInfo = info;
            Result = items.ToList();
        }
    }
}