#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services
{
	public class ProducersService : IProducerService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public ProducersService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
		{
			_unitOfWorkApi = unitOfWorkApi;
			_mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync() =>
				_mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(await _unitOfWorkApi.ProducersRepository
																									 .GetAllAsync());

		/// <inheritdoc />
		public async Task<ProducerReadDto> GetProducerByIdAsync(int producerId) =>
				_mapper.Map<Producer, ProducerReadDto>(await _unitOfWorkApi.ProducersRepository.GetByIdAsync(producerId));

		/// <inheritdoc />
		public async Task<bool> CreateProducer(ProducerInsertDto producer)
		{
			var mappedProducer = _mapper.Map<ProducerInsertDto, Producer>(producer);
			var canAdd = await _unitOfWorkApi.ProducersRepository.AnyAsync(mappedProducer);

			if (!canAdd) return false;

			await _unitOfWorkApi.ProducersRepository.AddAsync(mappedProducer);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateProducer(int id, ProducerUpdateDto producer)
		{
			var bdEntity = await _unitOfWorkApi.ProducersRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(producer, bdEntity);
			await _unitOfWorkApi.ProducersRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteProducer(int id)
		{
			var producerToDelete = await _unitOfWorkApi.ProducersRepository.GetByIdAsync(id);
			await _unitOfWorkApi.ProducersRepository.DeleteAsync(producerToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}
	}
}