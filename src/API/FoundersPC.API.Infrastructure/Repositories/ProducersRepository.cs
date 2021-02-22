#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories
{
    public class ProducersRepository : GenericRepositoryAsync<Producer>, IProducersRepositoryAsync
    {
        /// <inheritdoc />
        public ProducersRepository(FoundersPCHardwareContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IProducersRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<Producer>> GetAllWithHardwareAsync()
        {
            return await Context.Set<Producer>()
                                .Include(producer => producer.Motherboards)
                                .ThenInclude(motherboard => motherboard.Producer)
                                .Include(producer => producer.PowerSupplies)
                                .ThenInclude(powerSupply => powerSupply.Producer)
                                .Include(producer => producer.Cases)
                                .ThenInclude(@case => @case.Producer)
                                .Include(producer => producer.HardDrives)
                                .ThenInclude(hdd => hdd.Producer)
                                .Include(producer => producer.SolidStateDrive)
                                .ThenInclude(ssd => ssd.Producer)
                                .Include(producer => producer.Processors)
                                .ThenInclude(cpu => cpu.Producer)
                                .Include(producer => producer.Processors)
                                .ThenInclude(cpu => cpu.Core)
                                .Include(producer => producer.VideoCards)
                                .ThenInclude(videoCardCore => videoCardCore.Producer)
                                .Include(producer => producer.VideoCards)
                                .ThenInclude(gpu => gpu.Core)
                                .Include(producer => producer.RandomAccessMemory)
                                .ThenInclude(ram => ram.Producer)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Producer>> GetAllAsync() => await Context.Set<Producer>().ToListAsync();

        #endregion
    }
}