#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.VideoCard
{
    public class VideoCardsService : IVideoCardsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public VideoCardsService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<VideoCardReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<VideoCardReadDto>> GetPaginateableAsync(int pageNumber = 1,
                                                                                      int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<Domain.Entities.Hardware.VideoCard.VideoCard>, IEnumerable<VideoCardReadDto>>(await
                                                                                                         _unitOfWorkHardwareAPI
                                                                                                             .VideoCardsRepository
                                                                                                             .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.VideoCardsRepository.CountAsync();

            return new PaginationResponse<VideoCardReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IVideoCardsService

        /// <inheritdoc/>
        public async Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync() =>
            _mapper.Map<IEnumerable<Domain.Entities.Hardware.VideoCard.VideoCard>, IEnumerable<VideoCardReadDto>>(await
                                                                                                      _unitOfWorkHardwareAPI
                                                                                                          .VideoCardsRepository
                                                                                                          .GetAllAsync());

        /// <inheritdoc/>
        public async Task<VideoCardReadDto> GetVideoCardByIdAsync(int gpuId) =>
            _mapper.Map<Domain.Entities.Hardware.VideoCard.VideoCard, VideoCardReadDto>(await _unitOfWorkHardwareAPI
                                                                                              .VideoCardsRepository
                                                                                              .GetByIdAsync(gpuId));

        /// <inheritdoc/>
        public async Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard)
        {
            var mappedGPU = _mapper.Map<VideoCardInsertDto, Domain.Entities.Hardware.VideoCard.VideoCard>(videoCard);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.VideoCardsRepository.AnyAsync(x => x.Equals(mappedGPU));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.VideoCardsRepository.AddAsync(mappedGPU);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.VideoCardsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(videoCard, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.VideoCardsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteVideoCardAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.VideoCardsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}