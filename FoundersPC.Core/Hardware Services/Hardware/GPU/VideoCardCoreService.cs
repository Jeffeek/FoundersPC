using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
	public class VideoCardCoreService : IVideoCardCoreService
	{
		#region Implementation of IVideoCardCoreService

		/// <inheritdoc />
		public Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateVideoCardCore(VideoCardCoreInsertDto videoCardCore) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateVideoCardCore(int id, VideoCardCoreUpdateDto videoCardCore) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteVideoCardCore(int id) => throw new NotImplementedException();

		#endregion
	}
}
