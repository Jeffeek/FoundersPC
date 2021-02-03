#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services
{
	public class CPUService : ICPUService
	{
		private readonly IUnitOfWorkAsync _unitOfWork;
		private readonly IMapper _mapper;

		public CPUService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync() =>
			_mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(await _unitOfWork.ProcessorsRepository
				                                                       .GetAllAsync());

		/// <inheritdoc />
		public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId) =>
			_mapper.Map<CPU, CPUReadDto>(await _unitOfWork.ProcessorsRepository.GetByIdAsync(cpuId));

		/// <inheritdoc />
		public async Task<bool> CreateCPU(CPUInsertDto cpu)
		{
			var mappedCpu = _mapper.Map<CPUInsertDto, CPU>(cpu);
			await _unitOfWork.ProcessorsRepository.AddAsync(mappedCpu);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateCPU(int id, CPUUpdateDto cpu)
		{
			var bdEntity = await _unitOfWork.ProcessorsRepository.GetByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(cpu, bdEntity);
			await _unitOfWork.ProcessorsRepository.UpdateAsync(bdEntity);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> DeleteCPU(int id)
		{
			var cpuToDelete = await _unitOfWork.ProcessorsRepository.GetByIdAsync(id);
			await _unitOfWork.ProcessorsRepository.DeleteAsync(cpuToDelete);
			return await _unitOfWork.SaveChangesAsync();
		}
	}
}