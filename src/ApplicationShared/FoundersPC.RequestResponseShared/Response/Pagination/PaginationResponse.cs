#region Using namespaces

using System.Collections.Generic;

#endregion

namespace FoundersPC.RequestResponseShared.Response.Pagination
{
    public class PaginationResponse<T> : IPaginationResponse<T> where T : class
    {
        public PaginationResponse(IEnumerable<T> items, int totalItemsCount)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
        }

        #region Implementation of IPaginationResponse<T>

        /// <inheritdoc/>
        public int TotalItemsCount { get; }

        /// <inheritdoc/>
        public IEnumerable<T> Items { get; }

        #endregion
    }
}