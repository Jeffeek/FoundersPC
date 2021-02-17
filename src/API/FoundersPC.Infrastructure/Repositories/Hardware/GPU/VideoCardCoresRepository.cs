#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories.Hardware.GPU
{
	public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCore>, IVideoCardCoresRepositoryAsync
	{
		/// <inheritdoc />
		public VideoCardCoresRepository(DbContext context) : base(context) { }

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