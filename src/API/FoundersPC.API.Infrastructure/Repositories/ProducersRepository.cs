#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories
{
    public class ProducersRepository : GenericRepositoryAsync<Producer>, IProducersRepositoryAsync
    {
        /// <inheritdoc/>
        public ProducersRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<ProducerEntity>

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     source or keySelector is
        ///     <see langword="null"/>.
        /// </exception>
        public async Task<IEnumerable<Producer>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                .ToListAsync();

        #endregion

        #region Implementation of IProducersRepositoryAsync

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>
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

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public override async Task<IEnumerable<Producer>> GetAllAsync() =>
            await Context.Set<Producer>()
                         .ToListAsync();

        #endregion
    }
}