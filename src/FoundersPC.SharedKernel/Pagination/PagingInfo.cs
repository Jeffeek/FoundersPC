#region Using namespaces

using System;

#endregion

namespace FoundersPC.SharedKernel.Pagination;

public class PagingInfo : IPagingInfo
{
    public PagingInfo() { }

    public PagingInfo(int pageNumber, int pageSize, int totalItemCount)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItemCount = totalItemCount;
    }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItemCount { get; set; }

    public int PageCount
    {
        get
        {
            if (TotalItemCount <= 0)
                return 0;

            if (PageSize <= 0)
                PageSize = TotalItemCount;

            return (int)Math.Ceiling(TotalItemCount / (double)PageSize);
        }
    }
}