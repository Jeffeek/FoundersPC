#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware
{
    public class MotherboardsService : IMotherboardsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public MotherboardsService(IMapper mapper,
                                   IUnitOfWorkHardwareAPI uow)
        {
            _mapper = mapper;
            _unitOfWorkHardwareAPI = uow;
        }

        #region Implementation of IPaginateableService<MotherboardReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<MotherboardReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<Motherboard>, IEnumerable<MotherboardReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                         .MotherboardsRepository
                                                                                                         .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.MotherboardsRepository.CountAsync();

            return new PaginationResponse<MotherboardReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of IMotherboardsService

        /// <inheritdoc/>
        public async Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync() =>
            _mapper.Map<IEnumerable<Motherboard>, IEnumerable<MotherboardReadDto>>(await _unitOfWorkHardwareAPI
                                                                                               .MotherboardsRepository
                                                                                               .GetAllAsync());

        /// <inheritdoc/>
        public async Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboardId) =>
            _mapper.Map<Motherboard, MotherboardReadDto>(await _unitOfWorkHardwareAPI
                                                                     .MotherboardsRepository
                                                                     .GetByIdAsync(motherboardId));

        /// <inheritdoc/>
        public async Task<bool> CreateMotherboardAsync(MotherboardInsertDto motherboard)
        {
            var mappedMotherboard = _mapper.Map<MotherboardInsertDto, Motherboard>(motherboard);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.MotherboardsRepository.AnyAsync(x => x.Equals(mappedMotherboard));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.MotherboardsRepository.AddAsync(mappedMotherboard);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateMotherboardAsync(int id, MotherboardUpdateDto motherboard)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.MotherboardsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(motherboard, dataBaseEntity);
            var result = await _unitOfWorkHardwareAPI.MotherboardsRepository.UpdateAsync(dataBaseEntity);

            if (!result)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteMotherboardAsync(int id)
        {
            var result = await _unitOfWorkHardwareAPI.MotherboardsRepository.DeleteAsync(id);

            if (!result)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}