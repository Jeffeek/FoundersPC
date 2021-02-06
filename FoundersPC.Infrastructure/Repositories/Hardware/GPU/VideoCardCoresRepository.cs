#region Using derectives

using System.Linq;
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
		public async Task<VideoCardCore> GetByIdAsync(int id) =>
			await (await GetAllAsync()).FirstOrDefaultAsync(videoCardCore => videoCardCore.Id == id);

		/// <inheritdoc />
		public async Task<IQueryable<VideoCardCore>> GetAllAsync() =>
			await Task.Run(() => _context.Set<VideoCardCore>().Include(videoCardCore => videoCardCore.VideoCards));

		#endregion
	}
}