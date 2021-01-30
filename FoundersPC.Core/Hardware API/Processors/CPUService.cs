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
		public void CreateCPU(CPUReadDto cpu)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void UpdateCPU(CPUReadDto cpu)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void DeleteCPU(CPUReadDto cpu)
		{
			throw new NotImplementedException();
		}
	}
}
