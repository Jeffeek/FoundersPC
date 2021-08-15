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
    public class SolidStateDrivesService : ISolidStateDrivesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public SolidStateDrivesService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<SolidStateDriveReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<SolidStateDriveReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<SolidStateDrive>, IEnumerable<SolidStateDriveReadDto>>(await _unitOfWorkHardwareAPI
                .SolidStateDrivesRepository
                .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.CountAsync();

            return new PaginationResponse<SolidStateDriveReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of ISolidStateDrivesService

        /// <inheritdoc/>
        public async Task<IEnumerable<SolidStateDriveReadDto>> GetAllSolidStateDrivesAsync() =>
            _mapper.Map<IEnumerable<SolidStateDrive>, IEnumerable<SolidStateDriveReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                       .SolidStateDrivesRepository
                                                                                                       .GetAllAsync());

        /// <inheritdoc/>
        public async Task<SolidStateDriveReadDto> GetSolidStateDriveByIdAsync(int ssdId) =>
            _mapper.Map<SolidStateDrive, SolidStateDriveReadDto>(await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.GetByIdAsync(ssdId));

        /// <inheritdoc/>
        public async Task<bool> CreateSolidStateDriveAsync(SolidStateDriveInsertDto solidStateDrive)
        {
            var mappedSSD = _mapper.Map<SolidStateDriveInsertDto, SolidStateDrive>(solidStateDrive);
            var entityAlreadyExists = await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.AnyAsync(x => x.Equals(mappedSSD));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.AddAsync(mappedSSD);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateSolidStateDriveAsync(int id, SolidStateDriveUpdateDto solidStateDrive)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(solidStateDrive, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteSolidStateDriveAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.SolidStateDrivesRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}