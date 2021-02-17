#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
	public class VideoCardCoreService : IVideoCardCoreService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public VideoCardCoreService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		#region Implementation of IVideoCardCoreService

		/// <inheritdoc />
		public async Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync() => _mapper.Map<IEnumerable<VideoCardCore>, IEnumerable<VideoCardCoreReadDto>>(await _unitOfWork
																																											 .VideoCardCoresRepository
																																											 .GetAllAsync());

		/// <inheritdoc />
		public async Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId) => _mapper.Map<VideoCardCore, VideoCardCoreReadDto>(await _unitOfWork.VideoCardCoresRepository
																																									.GetByIdAsync(videoCardCoreId));

		/// <inheritdoc />
		public async Task<bool> CreateVideoCardCore(VideoCardCoreInsertDto videoCardCore)
		{
			var mappedVideoCardCore = _mapper.Map<VideoCardCoreInsertDto, VideoCardCore>(videoCardCore);
			await _unitOfWork.VideoCardCoresRepository.AddAsync(mappedVideoCardCore);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateVideoCardCore(int id, VideoCardCoreUpdateDto videoCardCore)
		{
			var bdEntity = await _unitOfWork.VideoCardCoresRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(videoCardCore, bdEntity);
			await _unitOfWork.VideoCardCoresRepository.UpdateAsync(bdEntity);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteVideoCardCore(int id)
		{
			var videoCardCoreToDelete = await _unitOfWork.VideoCardCoresRepository.GetByIdAsync(id);
			await _unitOfWork.VideoCardCoresRepository.DeleteAsync(videoCardCoreToDelete);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		#endregion
	}
}