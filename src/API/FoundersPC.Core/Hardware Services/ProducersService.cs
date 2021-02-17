#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services
{
	public class ProducersService : IProducerService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAsync _unitOfWork;

		public ProducersService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync() => _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(await _unitOfWork.ProducersRepository
																																									.GetAllAsync());

		/// <inheritdoc />
		public async Task<ProducerReadDto> GetProducerByIdAsync(int producerId) => _mapper.Map<Producer, ProducerReadDto>(await _unitOfWork.ProducersRepository.GetByIdAsync(producerId));

		/// <inheritdoc />
		public async Task<bool> CreateProducer(ProducerInsertDto producer)
		{
			var mappedProducer = _mapper.Map<ProducerInsertDto, Producer>(producer);
			var canAdd = await _unitOfWork.ProducersRepository.AnyAsync(mappedProducer);

			if (!canAdd) return false;

			await _unitOfWork.ProducersRepository.AddAsync(mappedProducer);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateProducer(int id, ProducerUpdateDto producer)
		{
			var bdEntity = await _unitOfWork.ProducersRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(producer, bdEntity);
			await _unitOfWork.ProducersRepository.UpdateAsync(bdEntity);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteProducer(int id)
		{
			var producerToDelete = await _unitOfWork.ProducersRepository.GetByIdAsync(id);
			await _unitOfWork.ProducersRepository.DeleteAsync(producerToDelete);

			return await _unitOfWork.SaveChangesAsync() > 0;
		}
	}
}