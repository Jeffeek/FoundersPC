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
	    public async Task<bool> CreateProducer(ProducerInsertDto producer)
	    {
			var mappedProducer = _mapper.Map<ProducerInsertDto, Producer>(producer);
			await _unitOfWork.ProducersRepository.CreateProducer(mappedProducer);
			return await _unitOfWork.SaveChangesAsync();
		}

	    /// <inheritdoc />
	    public async Task<bool> UpdateProducer(int id, ProducerUpdateDto producer)
	    {
			var bdEntity = await _unitOfWork.ProducersRepository.GetProducerByIdAsync(id);
			if (bdEntity == null) return false;
			_mapper.Map(producer, bdEntity);
			await _unitOfWork.ProducersRepository.UpdateProducer(bdEntity);
			return await _unitOfWork.SaveChangesAsync();
		}

	    /// <inheritdoc />
	    public async Task<bool> DeleteProducer(int id)
	    {
			var producerToDelete = await _unitOfWork.ProducersRepository.GetProducerByIdAsync(id);
			await _unitOfWork.ProducersRepository.DeleteProducer(producerToDelete);
			return await _unitOfWork.SaveChangesAsync();
		}
    }
}
