#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
	public class SSDService : ISSDService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public SSDService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		#region Implementation of ISSDService

		/// <inheritdoc />
		public async Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync() => _mapper.Map<IEnumerable<SSD>, IEnumerable<SSDReadDto>>(await _unitOfWork.SSDsRepository
																																				.GetAllAsync());

		/// <inheritdoc />
		public async Task<SSDReadDto> GetSSDByIdAsync(int ssdId) => _mapper.Map<SSD, SSDReadDto>(await _unitOfWork.SSDsRepository.GetByIdAsync(ssdId));

		/// <inheritdoc />
		public async Task<bool> CreateSSD(SSDInsertDto ssd)
		{
			var mappedSSD = _mapper.Map<SSDInsertDto, SSD>(ssd);
			await _unitOfWork.SSDsRepository.AddAsync(mappedSSD);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateSSD(int id, SSDUpdateDto ssd)
		{
			var bdEntity = await _unitOfWork.SSDsRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(ssd, bdEntity);
			await _unitOfWork.SSDsRepository.UpdateAsync(bdEntity);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteSSD(int id)
		{
			var ssdToDelete = await _unitOfWork.SSDsRepository.GetByIdAsync(id);
			await _unitOfWork.SSDsRepository.DeleteAsync(ssdToDelete);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		#endregion
	}
}