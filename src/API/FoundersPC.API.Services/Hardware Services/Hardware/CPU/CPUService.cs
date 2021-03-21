#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Infrastructure.UnitOfWork;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.CPU
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

        /// <inheritdoc />
        public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync() =>
            _mapper.Map<IEnumerable<Domain.Entities.Hardware.Processor.CPU>, IEnumerable<CPUReadDto>>(await _unitOfWorkHardwareAPI
                                                                                                          .ProcessorsRepository
                                                                                                          .GetAllAsync());

        /// <inheritdoc />
        public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId) =>
            _mapper.Map<Domain.Entities.Hardware.Processor.CPU, CPUReadDto>(await _unitOfWorkHardwareAPI
                                                                                  .ProcessorsRepository
                                                                                  .GetByIdAsync(cpuId));

        /// <inheritdoc />
        public async Task<bool> CreateCPUAsync(CPUInsertDto cpu)
        {
            var mappedCpu = _mapper.Map<CPUInsertDto, Domain.Entities.Hardware.Processor.CPU>(cpu);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.ProcessorsRepository.AnyAsync(x => x.Equals(mappedCpu));

            if (entityAlreadyExists) return false;

            await _unitOfWorkHardwareAPI.ProcessorsRepository.AddAsync(mappedCpu);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateCPUAsync(int id, CPUUpdateDto cpu)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.ProcessorsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null) return false;

            _mapper.Map(cpu, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCPUAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.DeleteAsync(id);

            if (!removeResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }
    }
}