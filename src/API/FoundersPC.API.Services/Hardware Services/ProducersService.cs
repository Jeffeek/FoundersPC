#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.API.Services.Hardware_Services
{
    public class ProducersService : IProducerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public ProducersService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync() =>
            _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(await _unitOfWorkHardwareAPI
                                                                                   .ProducersRepository
                                                                                   .GetAllAsync());

        /// <inheritdoc/>
        public async Task<ProducerReadDto> GetProducerByIdAsync(int producerId) =>
            _mapper.Map<Producer, ProducerReadDto>(await _unitOfWorkHardwareAPI.ProducersRepository
                                                                               .GetByIdAsync(producerId));

        /// <inheritdoc/>
        public async Task<bool> CreateProducerAsync(ProducerInsertDto producer)
        {
            var mappedProducer = _mapper.Map<ProducerInsertDto, Producer>(producer);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.ProducersRepository.AnyAsync(x => x.Equals(mappedProducer));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.ProducersRepository.AddAsync(mappedProducer);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.ProducersRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(producer, dataBaseEntity);
            await _unitOfWorkHardwareAPI.ProducersRepository.UpdateAsync(dataBaseEntity);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProducerAsync(int id)
        {
            var result = await _unitOfWorkHardwareAPI.ProducersRepository.DeleteAsync(id);

            if (!result)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #region Implementation of IPaginateableService<ProducerReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<ProducerReadDto>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerReadDto>>(await _unitOfWorkHardwareAPI
                                                                                               .ProducersRepository
                                                                                               .GetPaginateableAsync(pageNumber,
                                                                                                   pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.ProducersRepository.CountAsync();

            return new PaginationResponse<ProducerReadDto>(items, totalItemsCount);
        }

        #endregion
    }
}