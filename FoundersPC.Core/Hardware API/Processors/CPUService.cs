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

		public IEnumerable<CPUReadDto> GetAll() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(_unitOfWork.ProcessorsRepository.GetAll());

		/// <inheritdoc />
		public async Task<IEnumerable<CPUReadDto>> GetAllProducersAsync() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(await _unitOfWork.ProcessorsRepository.GetAllCPUAsync());

		/// <inheritdoc />
		public async Task<CPUReadDto> GetProducerByIdAsync(int producerId) => _mapper.Map<CPU, CPUReadDto>(await _unitOfWork.ProcessorsRepository.GetCPUByIdAsync(producerId));

		/// <inheritdoc />
		public void CreateProducer(CPUReadDto producer)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void UpdateProducer(CPUReadDto producer)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void DeleteProducer(CPUReadDto producer)
		{
			throw new NotImplementedException();
		}
	}
}
