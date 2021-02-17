#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
    public class RAMService : IRAMService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

        public RAMService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
        {
            _unitOfWorkApi = unitOfWorkApi;
            _mapper = mapper;
        }

        #region Implementation of IRAMService

        /// <inheritdoc />
        public async Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync() =>
            _mapper.Map<IEnumerable<RAM>, IEnumerable<RAMReadDto>>(await _unitOfWorkApi.RAMsRepository
                                                                                       .GetAllAsync());

        /// <inheritdoc />
        public async Task<RAMReadDto> GetRAMByIdAsync(int ramId) => _mapper.Map<RAM, RAMReadDto>(await _unitOfWorkApi.RAMsRepository.GetByIdAsync(ramId));

        /// <inheritdoc />
        public async Task<bool> CreateRAM(RAMInsertDto ram)
        {
            var mappedRAM = _mapper.Map<RAMInsertDto, RAM>(ram);
            await _unitOfWorkApi.RAMsRepository.AddAsync(mappedRAM);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateRAM(int id, RAMUpdateDto ram)
        {
            var bdEntity = await _unitOfWorkApi.RAMsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;

            _mapper.Map(ram, bdEntity);
            await _unitOfWorkApi.RAMsRepository.UpdateAsync(bdEntity);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRAM(int id)
        {
            var ramToDelete = await _unitOfWorkApi.RAMsRepository.GetByIdAsync(id);
            await _unitOfWorkApi.RAMsRepository.DeleteAsync(ramToDelete);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        #endregion
    }
}