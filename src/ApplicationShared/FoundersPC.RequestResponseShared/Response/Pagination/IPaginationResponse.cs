using System.Collections.Generic;

namespace FoundersPC.RequestResponseShared.Response.Pagination
{
    public interface IPaginationResponse<out T> where T : class
    {
        int TotalItemsCount { get; }

        IEnumerable<T> Items { get; }
    }
}
