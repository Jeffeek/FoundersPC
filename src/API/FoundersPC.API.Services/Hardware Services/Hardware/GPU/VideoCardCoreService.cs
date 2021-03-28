#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.GPU
{
    public class VideoCardCoreService : IVideoCardCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public VideoCardCoreService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IVideoCardCoreService

        /// <inheritdoc />
        public async Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync() =>
            _mapper.Map<IEnumerable<VideoCardCore>, IEnumerable<VideoCardCoreReadDto>>(await _unitOfWorkHardwareAPI
                .VideoCardCoresRepository
                .GetAllAsync());

        /// <inheritdoc />
        public async Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId) =>
            _mapper.Map<VideoCardCore, VideoCardCoreReadDto>(await _unitOfWorkHardwareAPI.VideoCardCoresRepository
                                                                 .GetByIdAsync(videoCardCoreId));

        /// <inheritdoc />
        public async Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore)
        {
            var mappedVideoCardCore = _mapper.Map<VideoCardCoreInsertDto, VideoCardCore>(videoCardCore);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.VideoCardCoresRepository.AnyAsync(x => x.Equals(mappedVideoCardCore));

            if (entityAlreadyExists) return false;

            await _unitOfWorkHardwareAPI.VideoCardCoresRepository.AddAsync(mappedVideoCardCore);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.VideoCardCoresRepository.GetByIdAsync(id);

            if (dataBaseEntity == null) return false;

            _mapper.Map(videoCardCore, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.VideoCardCoresRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteVideoCardCoreAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.VideoCardCoresRepository.DeleteAsync(id);

            if (!removeResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}