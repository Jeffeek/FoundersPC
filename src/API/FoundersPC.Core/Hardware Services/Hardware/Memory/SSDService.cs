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
    public class SSDService : ISSDService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

        public SSDService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
        {
            _unitOfWorkApi = unitOfWorkApi;
            _mapper = mapper;
        }

        #region Implementation of ISSDService

        /// <inheritdoc />
        public async Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync() =>
            _mapper.Map<IEnumerable<SSD>, IEnumerable<SSDReadDto>>(await _unitOfWorkApi.SSDsRepository
                                                                                       .GetAllAsync());

        /// <inheritdoc />
        public async Task<SSDReadDto> GetSSDByIdAsync(int ssdId) => _mapper.Map<SSD, SSDReadDto>(await _unitOfWorkApi.SSDsRepository.GetByIdAsync(ssdId));

        /// <inheritdoc />
        public async Task<bool> CreateSSD(SSDInsertDto ssd)
        {
            var mappedSSD = _mapper.Map<SSDInsertDto, SSD>(ssd);
            await _unitOfWorkApi.SSDsRepository.AddAsync(mappedSSD);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateSSD(int id, SSDUpdateDto ssd)
        {
            var bdEntity = await _unitOfWorkApi.SSDsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;

            _mapper.Map(ssd, bdEntity);
            await _unitOfWorkApi.SSDsRepository.UpdateAsync(bdEntity);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteSSD(int id)
        {
            var ssdToDelete = await _unitOfWorkApi.SSDsRepository.GetByIdAsync(id);
            await _unitOfWorkApi.SSDsRepository.DeleteAsync(ssdToDelete);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        #endregion
    }
}