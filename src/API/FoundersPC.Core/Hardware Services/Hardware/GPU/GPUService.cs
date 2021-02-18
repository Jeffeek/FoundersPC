#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
	public class GPUService : IGPUService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public GPUService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
		{
			_unitOfWorkApi = unitOfWorkApi;
			_mapper = mapper;
		}

		#region Implementation of IGPUService

		/// <inheritdoc />
		public async Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync() =>
				_mapper.Map<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>, IEnumerable<GPUReadDto>>(await _unitOfWorkApi
																												.VideoCardsRepository
																												.GetAllAsync());

		/// <inheritdoc />
		public async Task<GPUReadDto> GetGPUByIdAsync(int gpuId) => _mapper.Map<Domain.Entities.Hardware.VideoCard.GPU, GPUReadDto>(await _unitOfWorkApi
																																		  .VideoCardsRepository
																																		  .GetByIdAsync(gpuId));

		/// <inheritdoc />
		public async Task<bool> CreateGPU(GPUInsertDto gpu)
		{
			var mappedGPU = _mapper.Map<GPUInsertDto, Domain.Entities.Hardware.VideoCard.GPU>(gpu);
			await _unitOfWorkApi.VideoCardsRepository.AddAsync(mappedGPU);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateGPU(int id, GPUUpdateDto gpu)
		{
			var bdEntity = await _unitOfWorkApi.VideoCardsRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(gpu, bdEntity);
			await _unitOfWorkApi.VideoCardsRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteGPU(int id)
		{
			var gpuToDelete = await _unitOfWorkApi.VideoCardsRepository.GetByIdAsync(id);
			await _unitOfWorkApi.VideoCardsRepository.DeleteAsync(gpuToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		#endregion
	}
}