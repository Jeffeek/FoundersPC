#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination;

#endregion

namespace FoundersPC.ServicesShared
{
    public interface IPaginateableService<T> where T : class
    {
        Task<IPaginationResponse<T>> GetPaginateableAsync(int pageNumber, int pageSize);
    }
}