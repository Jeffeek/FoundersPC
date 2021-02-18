#region Using derectives

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
	public class MotherboardService : IMotherboardService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public MotherboardService(IMapper mapper,
								  IUnitOfWorkAPIAsync uow
		)
		{
			_mapper = mapper;
			_unitOfWorkApi = uow;
		}

		#region Implementation of IMotherboardService

		/// <inheritdoc />
		public async Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync() =>
				_mapper.Map<IEnumerable<Motherboard>, IEnumerable<MotherboardReadDto>>(await _unitOfWorkApi
																							 .MotherboardsRepository
																							 .GetAllAsync());

		/// <inheritdoc />
		public async Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboardId) => _mapper.Map<Motherboard, MotherboardReadDto>(await _unitOfWorkApi
																																			   .MotherboardsRepository
																																			   .GetByIdAsync(motherboardId));

		/// <inheritdoc />
		public async Task<bool> CreateMotherboard(MotherboardInsertDto motherboard)
		{
			var mappedMotherboard = _mapper.Map<MotherboardInsertDto, Motherboard>(motherboard);

			if (await _unitOfWorkApi.MotherboardsRepository.AnyAsync(mappedMotherboard)) return false;

			await _unitOfWorkApi.MotherboardsRepository.AddAsync(mappedMotherboard);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateMotherboard(int id, MotherboardUpdateDto motherboard)
		{
			var bdEntity = await _unitOfWorkApi.MotherboardsRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(motherboard, bdEntity);
			await _unitOfWorkApi.MotherboardsRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteMotherboard(int id)
		{
			var motherboardToDelete = await _unitOfWorkApi.MotherboardsRepository.GetByIdAsync(id);

			if (motherboardToDelete == null) return false;

			await _unitOfWorkApi.MotherboardsRepository.DeleteAsync(motherboardToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		#endregion
	}
}