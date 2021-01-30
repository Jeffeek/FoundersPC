using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models.Hardware;
using FoundersPC.Services.Repositories.UoW;

namespace FoundersPC.Core.Hardware_API.Processors
{
    public class CPUService : ICPUService
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CPUService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<CPUReadDto> GetAllCPUs() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(_unitOfWork.ProcessorsRepository.GetAll());

		/// <inheritdoc />
		public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(await _unitOfWork.ProcessorsRepository.GetAllCPUsAsync());

		/// <inheritdoc />
		public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId) => _mapper.Map<CPU, CPUReadDto>(await _unitOfWork.ProcessorsRepository.GetCPUByIdAsync(cpuId));

		/// <inheritdoc />
		public async Task<bool> CreateCPU(CPUInsertDto cpu)
		{
			var mappedCpu = _mapper.Map<CPUInsertDto, CPU>(cpu);
			await _unitOfWork.ProcessorsRepository.CreateCPU(mappedCpu);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateCPU(int id, CPUUpdateDto cpu)
		{
			var bdEntity = await _unitOfWork.ProcessorsRepository.GetCPUByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(cpu, bdEntity);
			await _unitOfWork.ProcessorsRepository.UpdateCPU(bdEntity);
			return await _unitOfWork.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<bool> DeleteCPU(int id)
		{
			var cpuToDelete = await _unitOfWork.ProcessorsRepository.GetCPUByIdAsync(id);
			await _unitOfWork.ProcessorsRepository.DeleteCPU(cpuToDelete);
			return await _unitOfWork.SaveChangesAsync();
		}
	}
}
