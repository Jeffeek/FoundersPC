using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models.Hardware;
using FoundersPC.Services.Repositories.UoW;

namespace FoundersPC.Core.Hardware_API.Producers
{
    public class ProducersService : IProducerService
    {
	    private readonly IUnitOfWork _unitOfWork;
	    private readonly IMapper _mapper;

	    public ProducersService(IUnitOfWork unitOfWork, IMapper mapper)
	    {
		    _unitOfWork = unitOfWork;
		    _mapper = mapper;
	    }

	    public IEnumerable<ProducerReadDto> GetAllProducers() => _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(_unitOfWork.ProducersRepository.GetAll());

	    /// <inheritdoc />
	    public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync() => _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(await _unitOfWork.ProducersRepository.GetAllProducersAsync());

		/// <inheritdoc />
		public async Task<ProducerReadDto> GetProducerByIdAsync(int producerId) => _mapper.Map<Producer, ProducerReadDto>(await _unitOfWork.ProducersRepository.GetProducerByIdAsync(producerId));

		/// <inheritdoc />
	    public void CreateProducer(ProducerReadDto producerRead)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void UpdateProducer(ProducerReadDto producerRead)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void DeleteProducer(ProducerReadDto producerRead)
	    {
		    throw new NotImplementedException();
	    }
    }
}
