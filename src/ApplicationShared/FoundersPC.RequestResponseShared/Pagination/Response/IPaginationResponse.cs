#region Using namespaces

using System.Collections.Generic;

#endregion

namespace FoundersPC.RequestResponseShared.Pagination.Response
{
    public interface IPaginationResponse<out T> where T : class
    {
        int TotalItemsCount { get; }

        IEnumerable<T> Items { get; }
    }
}