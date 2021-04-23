#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;
using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard
{
    public class VideoCardsRepository : GenericRepositoryAsync<VideoCardEntity>,
                                        IVideoCardsRepositoryAsync
    {
        /// <inheritdoc/>
        public VideoCardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<VideoCardEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<VideoCardEntity>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.ProducerEntity)
                  .Include(x => x.CoreEntity)
                  .ToListAsync();

        #endregion

        #region Implementation of IVideoCardsRepositoryAsync

        public override async Task<VideoCardEntity> GetByIdAsync(int id)
        {
            var gpu = await Context.Set<VideoCardEntity>()
                                   .FindAsync(id);

            if (gpu is null)
                return null;

            await Context.Entry(gpu)
                         .Reference(x => x.ProducerEntity)
                         .LoadAsync();

            await Context.Entry(gpu)
                         .Reference(x => x.CoreEntity)
                         .LoadAsync();

            return gpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<VideoCardEntity>> GetAllAsync()
        {
            return await Context
                         .Set<VideoCardEntity>()
                         .Include(gpu => gpu.ProducerEntity)
                         .Include(gpu => gpu.CoreEntity)
                         .ToListAsync();
        }

        #endregion
    }
}