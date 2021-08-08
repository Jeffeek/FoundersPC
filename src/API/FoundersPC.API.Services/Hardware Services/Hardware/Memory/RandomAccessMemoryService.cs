#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Memory
{
    public class RandomAccessMemoryService : IRandomAccessMemoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public RandomAccessMemoryService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<RandomAccessMemoryReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<RandomAccessMemoryReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<RandomAccessMemoryEntity>, IEnumerable<RandomAccessMemoryReadDto>>(await _unitOfWorkHardwareAPI
                .RandomAccessMemoryRepository
                .GetPaginateableAsync(pageNumber,
                                      pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.CountAsync();

            return new PaginationResponse<RandomAccessMemoryReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IRandomAccessMemoryService

        /// <inheritdoc/>
        public async Task<IEnumerable<RandomAccessMemoryReadDto>> GetAllRandomAccessMemoryAsync() =>
            _mapper.Map<IEnumerable<RandomAccessMemoryEntity>, IEnumerable<RandomAccessMemoryReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                           .RandomAccessMemoryRepository
                                                                                                           .GetAllAsync());

        /// <inheritdoc/>
        public async Task<RandomAccessMemoryReadDto> GetRandomAccessMemoryByIdAsync(int ramId) =>
            _mapper.Map<RandomAccessMemoryEntity, RandomAccessMemoryReadDto>(await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.GetByIdAsync(ramId));

        /// <inheritdoc/>
        public async Task<bool> CreateRandomAccessMemoryAsync(RandomAccessMemoryInsertDto randomAccessMemory)
        {
            var mappedRAM = _mapper.Map<RandomAccessMemoryInsertDto, RandomAccessMemoryEntity>(randomAccessMemory);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.AnyAsync(x => x.Equals(mappedRAM));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.AddAsync(mappedRAM);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateRandomAccessMemoryAsync(int id, RandomAccessMemoryUpdateDto randomAccessMemory)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(randomAccessMemory, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteRandomAccessMemoryAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.RandomAccessMemoryRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}