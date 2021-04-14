#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Memory
{
    public class RAMService : IRAMService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public RAMService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IPaginateableService<RAMReadDto>

        /// <inheritdoc/>
        public async Task<IEnumerable<RAMReadDto>>
            GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize) =>
            _mapper.Map<IEnumerable<RAM>, IEnumerable<RAMReadDto>>(await _unitOfWorkHardwareAPI
                                                                         .RAMsRepository
                                                                         .GetPaginateableAsync(pageNumber, pageSize));

        #endregion

        #region Implementation of IRAMService

        /// <inheritdoc/>
        public async Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync() =>
            _mapper.Map<IEnumerable<RAM>, IEnumerable<RAMReadDto>>(await _unitOfWorkHardwareAPI
                                                                         .RAMsRepository
                                                                         .GetAllAsync());

        /// <inheritdoc/>
        public async Task<RAMReadDto> GetRAMByIdAsync(int ramId) =>
            _mapper.Map<RAM, RAMReadDto>(await _unitOfWorkHardwareAPI.RAMsRepository.GetByIdAsync(ramId));

        /// <inheritdoc/>
        public async Task<bool> CreateRAMAsync(RAMInsertDto ram)
        {
            var mappedRAM = _mapper.Map<RAMInsertDto, RAM>(ram);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.RAMsRepository.AnyAsync(x => x.Equals(mappedRAM));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.RAMsRepository.AddAsync(mappedRAM);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateRAMAsync(int id, RAMUpdateDto ram)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.RAMsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(ram, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.RAMsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteRAMAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.RAMsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}