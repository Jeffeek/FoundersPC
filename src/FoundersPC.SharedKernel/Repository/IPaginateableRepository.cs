#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.SharedKernel.Repository;

public interface IPaginateableRepository<T> where T : class
{
    Task<IEnumerable<T>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10);
}