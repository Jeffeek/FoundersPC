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
    public class HDDService : IHDDService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public HDDService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Implementation of IHDDService

        /// <inheritdoc />
        public async Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync()
        {
            return _mapper.Map<IEnumerable<HDD>, IEnumerable<HDDReadDto>>(await _unitOfWork.HDDsRepository
                                                                                           .GetAllAsync());
        }

        /// <inheritdoc />
        public async Task<HDDReadDto> GetHDDByIdAsync(int hddId)
        {
            return _mapper.Map<HDD, HDDReadDto>(await _unitOfWork.HDDsRepository.GetByIdAsync(hddId));
        }

        /// <inheritdoc />
        public async Task<bool> CreateHDD(HDDInsertDto hdd)
        {
            var mappedHDD = _mapper.Map<HDDInsertDto, HDD>(hdd);
            await _unitOfWork.HDDsRepository.AddAsync(mappedHDD);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateHDD(int id, HDDUpdateDto hdd)
        {
            var bdEntity = await _unitOfWork.HDDsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;
            _mapper.Map(hdd, bdEntity);
            await _unitOfWork.HDDsRepository.UpdateAsync(bdEntity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteHDD(int id)
        {
            var hddToDelete = await _unitOfWork.HDDsRepository.GetByIdAsync(id);
            await _unitOfWork.HDDsRepository.DeleteAsync(hddToDelete);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        #endregion
    }
}