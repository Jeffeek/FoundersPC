#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware.VideoCard;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware.GPU
{
	public interface IVideoCardCoresRepositoryAsync
	{
		Task<VideoCardCore> AddAsync(VideoCardCore videoCardCore);

		Task UpdateAsync(VideoCardCore videoCardCore);

		Task DeleteAsync(VideoCardCore videoCardCore);

		Task<VideoCardCore> GetByIdAsync(int id);

		Task<IQueryable<VideoCardCore>> GetAllAsync();
	}
}