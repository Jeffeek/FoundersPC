#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories
{
    public class ProducersRepository : GenericRepositoryAsync<ProducerEntity>, IProducersRepositoryAsync
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
        public async Task<IEnumerable<ProducerEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                .ToListAsync();

        #endregion

        #region Implementation of IProducersRepositoryAsync

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="navigationPropertyPath"/>
        ///     is <see langword="null"/>.
        /// </exception>
        public async Task<IEnumerable<ProducerEntity>> GetAllWithHardwareAsync()
        {
            return await Context.Set<ProducerEntity>()
                                .Include(producer => producer.Motherboards)
                                .ThenInclude(motherboard => motherboard.ProducerEntity)
                                .Include(producer => producer.PowerSupplies)
                                .ThenInclude(powerSupply => powerSupply.ProducerEntity)
                                .Include(producer => producer.Cases)
                                .ThenInclude(@case => @case.ProducerEntity)
                                .Include(producer => producer.HardDrives)
                                .ThenInclude(hdd => hdd.ProducerEntity)
                                .Include(producer => producer.SolidStateDrive)
                                .ThenInclude(ssd => ssd.ProducerEntity)
                                .Include(producer => producer.Processors)
                                .ThenInclude(cpu => cpu.ProducerEntity)
                                .Include(producer => producer.Processors)
                                .ThenInclude(cpu => cpu.Core)
                                .Include(producer => producer.VideoCards)
                                .ThenInclude(videoCardCore => videoCardCore.ProducerEntity)
                                .Include(producer => producer.VideoCards)
                                .ThenInclude(gpu => gpu.CoreEntity)
                                .Include(producer => producer.RandomAccessMemory)
                                .ThenInclude(ram => ram.ProducerEntity)
                                .ToListAsync();
        }

        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public override async Task<IEnumerable<ProducerEntity>> GetAllAsync() =>
            await Context.Set<ProducerEntity>()
                         .ToListAsync();

        #endregion
    }
}