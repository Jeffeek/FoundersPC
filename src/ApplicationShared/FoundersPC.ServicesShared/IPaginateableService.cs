#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;

#endregion

namespace FoundersPC.ServicesShared
{
    public interface IPaginateableService<T> where T : class
    {
        Task<IEnumerable<T>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize);
    }
}