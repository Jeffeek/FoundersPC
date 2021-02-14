#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
	public class GPUService : IGPUService
	{
		private readonly IUnitOfWorkAsync _unitOfWork;
		private readonly IMapper _mapper;

		public GPUService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		#region Implementation of IGPUService

		/// <inheritdoc />
		public async Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync() =>
			_mapper.Map<IEnumerable<Domain.Entities.Hardware.VideoCard.GPU>, IEnumerable<GPUReadDto>>(await _unitOfWork
				.VideoCardsRepository
				.GetAllAsync());

		/// <inheritdoc />
		public async Task<GPUReadDto> GetGPUByIdAsync(int gpuId) =>
			_mapper.Map<Domain.Entities.Hardware.VideoCard.GPU, GPUReadDto>(await _unitOfWork.VideoCardsRepository
				                                                                .GetByIdAsync(gpuId));

		/// <inheritdoc />
		public async Task<bool> CreateGPU(GPUInsertDto gpu)
		{
			var mappedGPU = _mapper.Map<GPUInsertDto, Domain.Entities.Hardware.VideoCard.GPU>(gpu);
			await _unitOfWork.VideoCardsRepository.AddAsync(mappedGPU);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateGPU(int id, GPUUpdateDto gpu)
		{
			var bdEntity = await _unitOfWork.VideoCardsRepository.GetByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(gpu, bdEntity);
			await _unitOfWork.VideoCardsRepository.UpdateAsync(bdEntity);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteGPU(int id)
		{
			var gpuToDelete = await _unitOfWork.VideoCardsRepository.GetByIdAsync(id);
			await _unitOfWork.VideoCardsRepository.DeleteAsync(gpuToDelete);
			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		#endregion
	}
}