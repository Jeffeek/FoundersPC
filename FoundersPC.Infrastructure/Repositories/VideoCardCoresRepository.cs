using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Infrastructure.Repositories
{
	public class VideoCardCoresRepository : GenericRepositoryAsync<VideoCardCore>, IVideoCardCoresRepositoryAsync
	{
		/// <inheritdoc />
		public VideoCardCoresRepository(DbContext context) : base(context) { }
	}
}
