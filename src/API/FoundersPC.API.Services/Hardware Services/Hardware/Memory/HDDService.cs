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
    public class HDDService : IHDDService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public HDDService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<HDDReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<HDDReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<HDD>, IEnumerable<HDDReadDto>>(await _unitOfWorkHardwareAPI
                                                                                     .HDDsRepository
                                                                                     .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.HDDsRepository.CountAsync();

            return new PaginationResponse<HDDReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IHDDService

        /// <inheritdoc/>
        public async Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync() =>
            _mapper.Map<IEnumerable<HDD>, IEnumerable<HDDReadDto>>(await _unitOfWorkHardwareAPI
                                                                         .HDDsRepository
                                                                         .GetAllAsync());

        /// <inheritdoc/>
        public async Task<HDDReadDto> GetHDDByIdAsync(int hddId) =>
            _mapper.Map<HDD, HDDReadDto>(await _unitOfWorkHardwareAPI.HDDsRepository.GetByIdAsync(hddId));

        /// <inheritdoc/>
        public async Task<bool> CreateHDDAsync(HDDInsertDto hdd)
        {
            var mappedHDD = _mapper.Map<HDDInsertDto, HDD>(hdd);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.HDDsRepository.AnyAsync(x => x.Equals(mappedHDD));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.HDDsRepository.AddAsync(mappedHDD);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateHDDAsync(int id, HDDUpdateDto hdd)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.HDDsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(hdd, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.HDDsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteHDDAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.HDDsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}