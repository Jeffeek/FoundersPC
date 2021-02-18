#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
	public class HDDService : IHDDService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public HDDService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
		{
			_unitOfWorkApi = unitOfWorkApi;
			_mapper = mapper;
		}

		#region Implementation of IHDDService

		/// <inheritdoc />
		public async Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync() => _mapper.Map<IEnumerable<HDD>, IEnumerable<HDDReadDto>>(await _unitOfWorkApi
																																	 .HDDsRepository
																																	 .GetAllAsync());

		/// <inheritdoc />
		public async Task<HDDReadDto> GetHDDByIdAsync(int hddId) => _mapper.Map<HDD, HDDReadDto>(await _unitOfWorkApi.HDDsRepository.GetByIdAsync(hddId));

		/// <inheritdoc />
		public async Task<bool> CreateHDD(HDDInsertDto hdd)
		{
			var mappedHDD = _mapper.Map<HDDInsertDto, HDD>(hdd);
			await _unitOfWorkApi.HDDsRepository.AddAsync(mappedHDD);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateHDD(int id, HDDUpdateDto hdd)
		{
			var bdEntity = await _unitOfWorkApi.HDDsRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(hdd, bdEntity);
			await _unitOfWorkApi.HDDsRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteHDD(int id)
		{
			var hddToDelete = await _unitOfWorkApi.HDDsRepository.GetByIdAsync(id);
			await _unitOfWorkApi.HDDsRepository.DeleteAsync(hddToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		#endregion
	}
}