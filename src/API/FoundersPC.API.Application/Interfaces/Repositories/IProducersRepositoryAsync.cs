#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for <see cref="Producer"/> database access
    /// </summary>
    public interface IProducersRepositoryAsync : IRepositoryAsync<Producer>,
                                                 IPaginateableRepository<Producer>
    {
        Task<IEnumerable<Producer>> GetAllWithHardwareAsync();
    }
}