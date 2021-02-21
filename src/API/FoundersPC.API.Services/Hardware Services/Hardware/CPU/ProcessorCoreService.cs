﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.API.Infrastructure.UoW;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.CPU
{
    public class ProcessorCoreService : IProcessorCoreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public ProcessorCoreService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of IProcessorCoreService

        /// <inheritdoc />
        public async Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync() =>
            _mapper.Map<IEnumerable<ProcessorCore>, IEnumerable<ProcessorCoreReadDto>>(await _unitOfWorkHardwareAPI
                                                                                             .ProcessorCoresRepository.GetAllAsync());

        /// <inheritdoc />
        public async Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuCoreId) =>
            _mapper.Map<ProcessorCore, ProcessorCoreReadDto>(await _unitOfWorkHardwareAPI
                                                                   .ProcessorCoresRepository
                                                                   .GetByIdAsync(cpuCoreId));

        /// <inheritdoc />
        public async Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpuCore)
        {
            var mappedCpuCore = _mapper.Map<ProcessorCoreInsertDto, ProcessorCore>(cpuCore);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.ProcessorCoresRepository.AnyAsync(x => x.Equals(mappedCpuCore));

            if (entityAlreadyExists) return false;

            await _unitOfWorkHardwareAPI.ProcessorCoresRepository.AddAsync(mappedCpuCore);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpuCore)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.ProcessorCoresRepository.GetByIdAsync(id);

            if (dataBaseEntity == null) return false;

            _mapper.Map(cpuCore, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.ProcessorCoresRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteProcessorCore(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.ProcessorCoresRepository.DeleteAsync(id);

            if (!removeResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}