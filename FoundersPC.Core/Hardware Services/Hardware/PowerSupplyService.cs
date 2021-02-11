﻿#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware
{
	public class PowerSupplyService : IPowerSupplyService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public PowerSupplyService(IMapper mapper,
		                          IUnitOfWorkAsync uow)
		{
			_mapper = mapper;
			_unitOfWork = uow;
		}

		#region Implementation of IPowerSupplyService

		/// <inheritdoc />
		public async Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliersAsync() =>
			_mapper.Map<IEnumerable<PowerSupply>, IEnumerable<PowerSupplyReadDto>>(await _unitOfWork
				.PowerSuppliersRepository
				.GetAllAsync());

		/// <inheritdoc />
		public async Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId) =>
			_mapper.Map<PowerSupply, PowerSupplyReadDto>(await _unitOfWork.PowerSuppliersRepository
			                                                              .GetByIdAsync(powerSupplyId));

		/// <inheritdoc />
		public async Task<bool> CreatePowerSupply(PowerSupplyInsertDto powerSupply)
		{
			var mappedPowerSupply = _mapper.Map<PowerSupplyInsertDto, PowerSupply>(powerSupply);
			if (await _unitOfWork.PowerSuppliersRepository.AnyAsync(mappedPowerSupply))
				return false;

			await _unitOfWork.PowerSuppliersRepository.AddAsync(mappedPowerSupply);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdatePowerSupply(int id, PowerSupplyUpdateDto powerSupply)
		{
			var bdEntity = await _unitOfWork.PowerSuppliersRepository.GetByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(powerSupply, bdEntity);
			await _unitOfWork.PowerSuppliersRepository.UpdateAsync(bdEntity);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeletePowerSupply(int id)
		{
			var powerSupplyToDelete = await _unitOfWork.PowerSuppliersRepository.GetByIdAsync(id);
			if (powerSupplyToDelete == null) return false;
			await _unitOfWork.PowerSuppliersRepository.DeleteAsync(powerSupplyToDelete);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		#endregion
	}
}