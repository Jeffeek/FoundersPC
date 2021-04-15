﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Memory
{
    public class SSDService : ISSDService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public SSDService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<SSDReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<SSDReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<SSD>, IEnumerable<SSDReadDto>>(await _unitOfWorkHardwareAPI
                                                                                     .SSDsRepository
                                                                                     .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.SSDsRepository.CountAsync();

            return new PaginationResponse<SSDReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of ISSDService

        /// <inheritdoc/>
        public async Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync() =>
            _mapper.Map<IEnumerable<SSD>, IEnumerable<SSDReadDto>>(await _unitOfWorkHardwareAPI
                                                                         .SSDsRepository
                                                                         .GetAllAsync());

        /// <inheritdoc/>
        public async Task<SSDReadDto> GetSSDByIdAsync(int ssdId) =>
            _mapper.Map<SSD, SSDReadDto>(await _unitOfWorkHardwareAPI.SSDsRepository.GetByIdAsync(ssdId));

        /// <inheritdoc/>
        public async Task<bool> CreateSSDAsync(SSDInsertDto ssd)
        {
            var mappedSSD = _mapper.Map<SSDInsertDto, SSD>(ssd);
            var entityAlreadyExists = await _unitOfWorkHardwareAPI.SSDsRepository.AnyAsync(x => x.Equals(mappedSSD));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.SSDsRepository.AddAsync(mappedSSD);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateSSDAsync(int id, SSDUpdateDto ssd)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.SSDsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(ssd, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.SSDsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteSSDAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.SSDsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}