#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Memory
{
    public class HDDService : IHDDService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public HDDService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IHDDService

        /// <inheritdoc />
        public async Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync() =>
            _mapper.Map<IEnumerable<HDD>, IEnumerable<HDDReadDto>>(await _unitOfWorkHardwareAPI
                                                                         .HDDsRepository
                                                                         .GetAllAsync());

        /// <inheritdoc />
        public async Task<HDDReadDto> GetHDDByIdAsync(int hddId) =>
            _mapper.Map<HDD, HDDReadDto>(await _unitOfWorkHardwareAPI.HDDsRepository.GetByIdAsync(hddId));

        /// <inheritdoc />
        public async Task<bool> CreateHDDAsync(HDDInsertDto hdd)
        {
            var mappedHDD = _mapper.Map<HDDInsertDto, HDD>(hdd);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.HDDsRepository.AnyAsync(x => x.Equals(mappedHDD));

            if (entityAlreadyExists) return false;

            await _unitOfWorkHardwareAPI.HDDsRepository.AddAsync(mappedHDD);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateHDDAsync(int id, HDDUpdateDto hdd)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.HDDsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null) return false;

            _mapper.Map(hdd, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.HDDsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteHDDAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.HDDsRepository.DeleteAsync(id);

            if (!removeResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}