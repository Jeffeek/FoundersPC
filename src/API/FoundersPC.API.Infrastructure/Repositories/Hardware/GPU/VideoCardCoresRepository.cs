#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.ApplicationShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Infrastructure.Repositories.Hardware.GPU
{
    public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCore>, IVideoCardCoresRepositoryAsync
    {
        /// <inheritdoc />
        public VideoCardCoresRepository(FoundersPCHardwareContext context) : base(context) { }

        #region Implementation of IVideoCardCoresRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<VideoCardCore>> GetAllAsync()
        {
            return await Context.Set<VideoCardCore>()
                                .Include(videoCardCore =>
                                             videoCardCore.VideoCards)
                                .ToListAsync();
        }

        #endregion
    }
}