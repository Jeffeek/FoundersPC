using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models;
using FoundersPC.Services.Repositories;

namespace FoundersPC.Core.Requests.Producers
{
    public class ProducersRequest : IProducerRequest
    {
	    private readonly IUnitOfWork _unitOfWork;
	    private readonly IMapper _mapper;

	    public ProducersRequest(IUnitOfWork unitOfWork, IMapper mapper)
	    {
		    _unitOfWork = unitOfWork;
		    _mapper = mapper;
	    }

	    public IEnumerable<ProducerDto> GetAll() => _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerDto>>(_unitOfWork.ProducersRepository.GetAll());

	    /// <inheritdoc />
	    public async Task<IEnumerable<ProducerDto>> GetAllProducersAsync() => _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerDto>>(await _unitOfWork.ProducersRepository.GetAllProducersAsync());

		/// <inheritdoc />
		public async Task<ProducerDto> GetProducerByIdAsync(int producerId) => _mapper.Map<Producer, ProducerDto>(await _unitOfWork.ProducersRepository.GetProducerByIdAsync(producerId));

		/// <inheritdoc />
	    public void CreateProducer(ProducerDto producer)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void UpdateProducer(ProducerDto producer)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void DeleteProducer(ProducerDto producer)
	    {
		    throw new NotImplementedException();
	    }
    }
}
