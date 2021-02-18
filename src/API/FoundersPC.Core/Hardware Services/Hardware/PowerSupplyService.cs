#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware
{
    public class PowerSupplyService : IPowerSupplyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

        public PowerSupplyService(IMapper mapper,
                                  IUnitOfWorkAPIAsync uow
        )
        {
            _mapper = mapper;
            _unitOfWorkApi = uow;
        }

        #region Implementation of IPowerSupplyService

        /// <inheritdoc />
        public async Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliersAsync() =>
            _mapper.Map<IEnumerable<PowerSupply>, IEnumerable<PowerSupplyReadDto>>(await _unitOfWorkApi
                                                                                         .PowerSuppliersRepository
                                                                                         .GetAllAsync());

        /// <inheritdoc />
        public async Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId) =>
            _mapper.Map<PowerSupply, PowerSupplyReadDto>(await _unitOfWorkApi
                                                               .PowerSuppliersRepository
                                                               .GetByIdAsync(powerSupplyId));

        /// <inheritdoc />
        public async Task<bool> CreatePowerSupply(PowerSupplyInsertDto powerSupply)
        {
            var mappedPowerSupply = _mapper.Map<PowerSupplyInsertDto, PowerSupply>(powerSupply);

            if (await _unitOfWorkApi.PowerSuppliersRepository.AnyAsync(mappedPowerSupply)) return false;

            await _unitOfWorkApi.PowerSuppliersRepository.AddAsync(mappedPowerSupply);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdatePowerSupply(int id, PowerSupplyUpdateDto powerSupply)
        {
            var bdEntity = await _unitOfWorkApi.PowerSuppliersRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;

            _mapper.Map(powerSupply, bdEntity);
            await _unitOfWorkApi.PowerSuppliersRepository.UpdateAsync(bdEntity);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeletePowerSupply(int id)
        {
            var powerSupplyToDelete = await _unitOfWorkApi.PowerSuppliersRepository.GetByIdAsync(id);

            if (powerSupplyToDelete == null) return false;

            await _unitOfWorkApi.PowerSuppliersRepository.DeleteAsync(powerSupplyToDelete);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        #endregion
    }
}