#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware
{
    public class PowerSupplyService : IPowerSupplyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public PowerSupplyService(IMapper mapper,
                                  IUnitOfWorkHardwareAPI uow)
        {
            _mapper = mapper;
            _unitOfWorkHardwareAPI = uow;
        }

        #region Implementation of IPaginateableService<PowerSupplyReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<PowerSupplyReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<PowerSupply>, IEnumerable<PowerSupplyReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                     .PowerSuppliersRepository
                                                                                                     .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.PowerSuppliersRepository.CountAsync();

            return new PaginationResponse<PowerSupplyReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IPowerSupplyService

        /// <inheritdoc/>
        public async Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync() =>
            _mapper.Map<IEnumerable<PowerSupply>, IEnumerable<PowerSupplyReadDto>>(await _unitOfWorkHardwareAPI
                                                                                         .PowerSuppliersRepository
                                                                                         .GetAllAsync());

        /// <inheritdoc/>
        public async Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId) =>
            _mapper.Map<PowerSupply, PowerSupplyReadDto>(await _unitOfWorkHardwareAPI
                                                               .PowerSuppliersRepository
                                                               .GetByIdAsync(powerSupplyId));

        /// <inheritdoc/>
        public async Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto powerSupply)
        {
            var mappedPowerSupply = _mapper.Map<PowerSupplyInsertDto, PowerSupply>(powerSupply);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.PowerSuppliersRepository.AnyAsync(x => x.Equals(mappedPowerSupply));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.PowerSuppliersRepository.AddAsync(mappedPowerSupply);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.PowerSuppliersRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(powerSupply, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.PowerSuppliersRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletePowerSupplyAsync(int id)
        {
            var result = await _unitOfWorkHardwareAPI.PowerSuppliersRepository.DeleteAsync(id);

            if (!result)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}