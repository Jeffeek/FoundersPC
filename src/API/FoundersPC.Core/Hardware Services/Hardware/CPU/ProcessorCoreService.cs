#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.CPU
{
	public class ProcessorCoreService : IProcessorCoreService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public ProcessorCoreService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
		{
			_unitOfWorkApi = unitOfWorkApi;
			_mapper = mapper;
		}

		#region Implementation of IProcessorCoreService

		/// <inheritdoc />
		public async Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync() =>
				_mapper.Map<IEnumerable<ProcessorCore>, IEnumerable<ProcessorCoreReadDto>>(await _unitOfWorkApi
																								 .ProcessorCoresRepository.GetAllAsync());

		/// <inheritdoc />
		public async Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId) =>
				_mapper.Map<ProcessorCore, ProcessorCoreReadDto>(await _unitOfWorkApi
																	   .ProcessorCoresRepository
																	   .GetByIdAsync(cpuCoreId));

		/// <inheritdoc />
		public async Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpuCore)
		{
			var mappedCpuCore = _mapper.Map<ProcessorCoreInsertDto, ProcessorCore>(cpuCore);

			if (await _unitOfWorkApi.ProcessorCoresRepository.AnyAsync(mappedCpuCore)) return false;

			await _unitOfWorkApi.ProcessorCoresRepository.AddAsync(mappedCpuCore);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpuCore)
		{
			var bdEntity = await _unitOfWorkApi.ProcessorCoresRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(cpuCore, bdEntity);
			await _unitOfWorkApi.ProcessorCoresRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteProcessorCore(int id)
		{
			var cpuCoreToDelete = await _unitOfWorkApi.ProcessorCoresRepository.GetByIdAsync(id);

			if (cpuCoreToDelete == null) return false;

			await _unitOfWorkApi.ProcessorCoresRepository.DeleteAsync(cpuCoreToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		#endregion
	}
}