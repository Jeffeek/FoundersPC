#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware
{
    public interface IProducersRepositoryAsync : IRepositoryAsync<Producer>,
                                                 IPaginateableRepository<Producer>
    {
        Task<IEnumerable<Producer>> GetAllWithHardwareAsync();
    }
}