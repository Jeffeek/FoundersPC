#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.ApplicationShared;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using FoundersPC.Infrastructure.API.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.API.Repositories.Hardware.GPU
{
    public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCore>, IVideoCardCoresRepositoryAsync
    {
        /// <inheritdoc />
        public VideoCardCoresRepository(FoundersPCDbContext context) : base(context) { }

        #region Implementation of IVideoCardCoresRepositoryAsync

        /// <inheritdoc />
        public async Task<IEnumerable<VideoCardCore>> GetAllAsync()
        {
            return await _context.Set<VideoCardCore>()
                                 .Include(videoCardCore =>
                                              videoCardCore.VideoCards)
                                 .ToListAsync();
        }

        #endregion
    }
}