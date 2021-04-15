#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Processor
{
    public class CPUService : ICPUService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public CPUService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync() =>
            _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(await
                                                                       _unitOfWorkHardwareAPI
                                                                           .ProcessorsRepository
                                                                           .GetAllAsync());

        /// <inheritdoc/>
        public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId) =>
            _mapper.Map<CPU, CPUReadDto>(await _unitOfWorkHardwareAPI
                                               .ProcessorsRepository
                                               .GetByIdAsync(cpuId));

        /// <inheritdoc/>
        public async Task<bool> CreateCPUAsync(CPUInsertDto cpu)
        {
            var mappedCpu = _mapper.Map<CPUInsertDto, CPU>(cpu);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.ProcessorsRepository.AnyAsync(x => x.Equals(mappedCpu));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.ProcessorsRepository.AddAsync(mappedCpu);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCPUAsync(int id, CPUUpdateDto cpu)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.ProcessorsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(cpu, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCPUAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #region Implementation of IPaginateableService<CPUReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<CPUReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<CPU>, IEnumerable<CPUReadDto>>(await _unitOfWorkHardwareAPI.ProcessorsRepository
                                                                                   .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.ProcessorsRepository.CountAsync();

            return new PaginationResponse<CPUReadDto>(items, totalItemsCount);
        }

        #endregion
    }
}