#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Memory
{
    public class HardDriveDisksService : IHardDriveDisksService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public HardDriveDisksService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<HardDriveDiskReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<HardDriveDiskReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<HardDriveDisk>, IEnumerable<HardDriveDiskReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                             .HardDrivesRepository
                                                                                                             .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.HardDrivesRepository.CountAsync();

            return new PaginationResponse<HardDriveDiskReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IHardDriveDisksService

        /// <inheritdoc/>
        public async Task<IEnumerable<HardDriveDiskReadDto>> GetAllHardDiskDrivesAsync() =>
            _mapper.Map<IEnumerable<HardDriveDisk>, IEnumerable<HardDriveDiskReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                   .HardDrivesRepository
                                                                                                   .GetAllAsync());

        /// <inheritdoc/>
        public async Task<HardDriveDiskReadDto> GetHardDiskDriveByIdAsync(int hddId) =>
            _mapper.Map<HardDriveDisk, HardDriveDiskReadDto>(await _unitOfWorkHardwareAPI.HardDrivesRepository.GetByIdAsync(hddId));

        /// <inheritdoc/>
        public async Task<bool> CreateHardDriveDiskAsync(HardDriveDiskInsertDto hardDriveDisk)
        {
            var mappedHDD = _mapper.Map<HardDriveDiskInsertDto, HardDriveDisk>(hardDriveDisk);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.HardDrivesRepository.AnyAsync(x => x.Equals(mappedHDD));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.HardDrivesRepository.AddAsync(mappedHDD);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateHardDriveDiskAsync(int id, HardDriveDiskUpdateDto hardDriveDisk)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.HardDrivesRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(hardDriveDisk, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.HardDrivesRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteHardDriveDiskAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.HardDrivesRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}