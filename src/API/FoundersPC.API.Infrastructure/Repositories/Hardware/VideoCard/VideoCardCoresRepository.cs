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
    public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCoreEntity>,
                                            IVideoCardCoresRepositoryAsync
    {
        /// <inheritdoc/>
        public VideoCardCoresRepository(DbContext context) : base(context) { }

        #region Implementation of IVideoCardCoresRepositoryAsync

        /// <inheritdoc/>
        public override async Task<IEnumerable<VideoCardCoreEntity>> GetAllAsync()
        {
            return await Context.Set<VideoCardCoreEntity>()
                                .Include(videoCardCore =>
                                             videoCardCore.VideoCards)
                                .ToListAsync();
        }

        #endregion

        #region Implementation of IPaginateableRepository<VideoCardCoreEntity>

        /// <inheritdoc/>
        public async Task<IEnumerable<VideoCardCoreEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.VideoCards)
                  .ToListAsync();

        #endregion
    }
}