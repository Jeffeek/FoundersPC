#region Using namespaces

using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.RequestResponseShared.Pagination.Requests
{
    public class PaginationRequest : IPaginationRequest
    {
        public PaginationRequest() : this(1, FoundersPCConstants.PageSize) { }

        public PaginationRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        #region Implementation of IPaginationRequest

        /// <inheritdoc/>
        [FromQuery(Name = "Page")]
        public int PageNumber { get; set; }

        /// <inheritdoc/>
        [FromQuery(Name = "Size")]
        public int PageSize { get; set; }

        #endregion
    }
}