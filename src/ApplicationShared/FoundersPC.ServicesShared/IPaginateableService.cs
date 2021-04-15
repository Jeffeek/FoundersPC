#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.ServicesShared
{
    public interface IPaginateableService<T> where T : class
    {
        Task<IPaginationResponse<T>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize);
    }
}