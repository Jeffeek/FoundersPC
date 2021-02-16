#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Domain.Entities.Hardware.Memory;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
    public class RAMService : IRAMService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public RAMService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Implementation of IRAMService

        /// <inheritdoc />
        public async Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync()
        {
            return _mapper.Map<IEnumerable<RAM>, IEnumerable<RAMReadDto>>(await _unitOfWork.RAMsRepository
                                                                                           .GetAllAsync());
        }

        /// <inheritdoc />
        public async Task<RAMReadDto> GetRAMByIdAsync(int ramId)
        {
            return _mapper.Map<RAM, RAMReadDto>(await _unitOfWork.RAMsRepository.GetByIdAsync(ramId));
        }

        /// <inheritdoc />
        public async Task<bool> CreateRAM(RAMInsertDto ram)
        {
            var mappedRAM = _mapper.Map<RAMInsertDto, RAM>(ram);
            await _unitOfWork.RAMsRepository.AddAsync(mappedRAM);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateRAM(int id, RAMUpdateDto ram)
        {
            var bdEntity = await _unitOfWork.RAMsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;
            _mapper.Map(ram, bdEntity);
            await _unitOfWork.RAMsRepository.UpdateAsync(bdEntity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteRAM(int id)
        {
            var ramToDelete = await _unitOfWork.RAMsRepository.GetByIdAsync(id);
            await _unitOfWork.RAMsRepository.DeleteAsync(ramToDelete);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion
    }
}