#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware
{
    public interface IProducersRepositoryAsync : IRepositoryAsync<Producer>
    {
        Task<IEnumerable<Producer>> GetAllWithHardwareAsync();
    }
}