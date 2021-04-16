﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.VideoCard
{
    public class GPUService : IGPUService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public GPUService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<GPUReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<GPUReadDto>> GetPaginateableAsync(int pageNumber = 1,
                                                                                int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<GPU>, IEnumerable<GPUReadDto>>(await
                                                                                   _unitOfWorkHardwareAPI
                                                                                       .VideoCardsRepository
                                                                                       .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.VideoCardsRepository.CountAsync();

            return new PaginationResponse<GPUReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IGPUService

        /// <inheritdoc/>
        public async Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync() =>
            _mapper.Map<IEnumerable<GPU>, IEnumerable<GPUReadDto>>(await
                                                                       _unitOfWorkHardwareAPI
                                                                           .VideoCardsRepository
                                                                           .GetAllAsync());

        /// <inheritdoc/>
        public async Task<GPUReadDto> GetGPUByIdAsync(int gpuId) =>
            _mapper.Map<GPU, GPUReadDto>(await _unitOfWorkHardwareAPI
                                               .VideoCardsRepository
                                               .GetByIdAsync(gpuId));

        /// <inheritdoc/>
        public async Task<bool> CreateGPUAsync(GPUInsertDto gpu)
        {
            var mappedGPU = _mapper.Map<GPUInsertDto, GPU>(gpu);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.VideoCardsRepository.AnyAsync(x => x.Equals(mappedGPU));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.VideoCardsRepository.AddAsync(mappedGPU);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateGPUAsync(int id, GPUUpdateDto gpu)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.VideoCardsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(gpu, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.VideoCardsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteGPUAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.VideoCardsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}