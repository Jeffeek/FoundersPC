#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Domain.Entities.Hardware.VideoCard;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
    public class VideoCardCoreService : IVideoCardCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

        public VideoCardCoreService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
        {
            _unitOfWorkApi = unitOfWorkApi;
            _mapper = mapper;
        }

        #region Implementation of IVideoCardCoreService

        /// <inheritdoc />
        public async Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync() =>
            _mapper.Map<IEnumerable<VideoCardCore>, IEnumerable<VideoCardCoreReadDto>>(await _unitOfWorkApi
                                                                                             .VideoCardCoresRepository
                                                                                             .GetAllAsync());

        /// <inheritdoc />
        public async Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId) =>
            _mapper.Map<VideoCardCore, VideoCardCoreReadDto>(await _unitOfWorkApi.VideoCardCoresRepository
                                                                                 .GetByIdAsync(videoCardCoreId));

        /// <inheritdoc />
        public async Task<bool> CreateVideoCardCore(VideoCardCoreInsertDto videoCardCore)
        {
            var mappedVideoCardCore = _mapper.Map<VideoCardCoreInsertDto, VideoCardCore>(videoCardCore);
            await _unitOfWorkApi.VideoCardCoresRepository.AddAsync(mappedVideoCardCore);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateVideoCardCore(int id, VideoCardCoreUpdateDto videoCardCore)
        {
            var bdEntity = await _unitOfWorkApi.VideoCardCoresRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;

            _mapper.Map(videoCardCore, bdEntity);
            await _unitOfWorkApi.VideoCardCoresRepository.UpdateAsync(bdEntity);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteVideoCardCore(int id)
        {
            var videoCardCoreToDelete = await _unitOfWorkApi.VideoCardCoresRepository.GetByIdAsync(id);
            await _unitOfWorkApi.VideoCardCoresRepository.DeleteAsync(videoCardCoreToDelete);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        #endregion
    }
}