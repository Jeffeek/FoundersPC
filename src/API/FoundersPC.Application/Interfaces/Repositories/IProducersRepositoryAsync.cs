#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories
{
    public interface IProducersRepositoryAsync : IRepositoryAsync<Producer>
    {
        Task<IEnumerable<Producer>> GetAllWithHardwareAsync();
    }
}