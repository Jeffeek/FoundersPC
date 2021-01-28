using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models;
using FoundersPC.Services.Repositories;

namespace FoundersPC.Core.Requests.Processors
{
    public class CPURequest : ICPURequest
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CPURequest(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<CPUDto> GetAll() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUDto>>(_unitOfWork.ProcessorsRepository.GetAll());

		/// <inheritdoc />
		public async Task<IEnumerable<CPUDto>> GetAllProducersAsync() => _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUDto>>(await _unitOfWork.ProcessorsRepository.GetAllCPUAsync());

		/// <inheritdoc />
		public async Task<CPUDto> GetProducerByIdAsync(int producerId) => _mapper.Map<CPU, CPUDto>(await _unitOfWork.ProcessorsRepository.GetCPUByIdAsync(producerId));

		/// <inheritdoc />
		public void CreateProducer(CPUDto producer)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void UpdateProducer(CPUDto producer)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void DeleteProducer(CPUDto producer)
		{
			throw new NotImplementedException();
		}
	}
}
