#region Using namespaces

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace FoundersPC.SharedKernel.Pagination;

public class PagedList<T> : IPagedList<T>
{
    public PagedList(IQueryable<T> items, int pageNumber, int pageSize)
    {
        var totalItemCount = items?.Count() ?? 0;

        if (pageSize <= 0)
            pageSize = totalItemCount;

        if (pageNumber < 0)
            pageNumber = 0;

        PagingInfo = new(pageNumber, pageSize, totalItemCount);

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

    public PagingInfo PagingInfo { get; }

    public List<T> Result { get; }

    public IEnumerator<T> GetEnumerator() => Result.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}