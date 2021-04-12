#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.VideoCard;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard
{
    public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCore>,
                                            IVideoCardCoresRepositoryAsync
    {
        /// <inheritdoc/>
        public VideoCardCoresRepository(DbContext context) : base(context) { }

        #region Implementation of IVideoCardCoresRepositoryAsync

        /// <inheritdoc/>
        public override async Task<IEnumerable<VideoCardCore>> GetAllAsync()
        {
            return await Context.Set<VideoCardCore>()
                                .Include(videoCardCore =>
                                             videoCardCore.VideoCards)
                                .ToListAsync();
        }

        #endregion

        #region Implementation of IPaginateableRepository<VideoCardCore>

        /// <inheritdoc/>
        public async Task<IEnumerable<VideoCardCore>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            await GetPaginateableInternal(pageNumber, pageSize)
                  .Include(x => x.VideoCards)
                  .ToListAsync();

        #endregion
    }
}