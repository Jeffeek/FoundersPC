#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Hardware_Services
{
	public class ProcessorCoreService : IProcessorCoreService
	{
		private readonly IUnitOfWorkAsync _unitOfWork;
		private readonly IMapper _mapper;

		public ProcessorCoreService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		#region Implementation of IProcessorCoreService

		/// <inheritdoc />
		public async Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync() =>
			_mapper.Map<IEnumerable<ProcessorCore>, IEnumerable<ProcessorCoreReadDto>>(await _unitOfWork
				.ProcessorCoresRepository.GetAllAsync());

		/// <inheritdoc />
		public async Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId) =>
			_mapper.Map<ProcessorCore, ProcessorCoreReadDto>(await _unitOfWork.ProcessorCoresRepository.GetByIdAsync(cpuCoreId));

		/// <inheritdoc />
		public async Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpuCore)
		{
			var mappedCpuCore = _mapper.Map<ProcessorCoreInsertDto, ProcessorCore>(cpuCore);
			if (
				await (await _unitOfWork.ProcessorCoresRepository
				                        .GetAllAsync())
					.AnyAsync(x => x.Equals(mappedCpuCore)))
				return false;

			await _unitOfWork.ProcessorCoresRepository.AddAsync(mappedCpuCore);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpuCore)
		{
			var bdEntity = await _unitOfWork.ProcessorCoresRepository.GetByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(cpuCore, bdEntity);
			await _unitOfWork.ProcessorCoresRepository.UpdateAsync(bdEntity);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> DeleteProcessorCore(int id)
		{
			var cpuCoreToDelete = await _unitOfWork.ProcessorCoresRepository.GetByIdAsync(id);
			if (cpuCoreToDelete == null) return false;
			await _unitOfWork.ProcessorCoresRepository.DeleteAsync(cpuCoreToDelete);
			return await _unitOfWork.SaveChangesAsync();
		}

		#endregion
	}
}