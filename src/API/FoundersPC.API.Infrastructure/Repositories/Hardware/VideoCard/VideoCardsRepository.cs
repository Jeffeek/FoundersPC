#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard
{
    public class VideoCardsRepository : GenericRepositoryAsync<Domain.Entities.Hardware.VideoCard.VideoCard>,
                                        IVideoCardsRepositoryAsync
    {
        /// <inheritdoc/>
        public VideoCardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

        #region Implementation of IPaginateableRepository<VideoCardEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.VideoCard>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.Producer)
                  .Include(x => x.Core)
                  .ToListAsync();

        #endregion

        #region Implementation of IVideoCardsRepositoryAsync

        public override async Task<Domain.Entities.Hardware.VideoCard.VideoCard> GetByIdAsync(int id)
        {
            var gpu = await Context.Set<Domain.Entities.Hardware.VideoCard.VideoCard>()
                                   .FindAsync(id);

            if (gpu is null)
                return null;

            await Context.Entry(gpu)
                         .Reference(x => x.Producer)
                         .LoadAsync();

            await Context.Entry(gpu)
                         .Reference(x => x.Core)
                         .LoadAsync();

            return gpu;
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<Domain.Entities.Hardware.VideoCard.VideoCard>> GetAllAsync()
        {
            return await Context
                         .Set<Domain.Entities.Hardware.VideoCard.VideoCard>()
                         .Include(gpu => gpu.Producer)
                         .Include(gpu => gpu.Core)
                         .ToListAsync();
        }

        #endregion
    }
}