#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.CPU
{
    public class CPUService : ICPUService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

        public CPUService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
        {
            _unitOfWorkApi = unitOfWorkApi;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync() =>
            _mapper.Map<IEnumerable<Domain.Entities.Hardware.Processor.CPU>, IEnumerable<CPUReadDto>>(await _unitOfWorkApi
                                                                                                          .ProcessorsRepository
                                                                                                          .GetAllAsync());

        /// <inheritdoc />
        public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId) =>
            _mapper.Map<Domain.Entities.Hardware.Processor.CPU, CPUReadDto>(await _unitOfWorkApi
                                                                                  .ProcessorsRepository
                                                                                  .GetByIdAsync(cpuId));

        /// <inheritdoc />
        public async Task<bool> CreateCPU(CPUInsertDto cpu)
        {
            var mappedCpu = _mapper.Map<CPUInsertDto, Domain.Entities.Hardware.Processor.CPU>(cpu);

            if (await _unitOfWorkApi.ProcessorsRepository.AnyAsync(mappedCpu)) return false;

            await _unitOfWorkApi.ProcessorsRepository.AddAsync(mappedCpu);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateCPU(int id, CPUUpdateDto cpu)
        {
            var bdEntity = await _unitOfWorkApi.ProcessorsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;

            _mapper.Map(cpu, bdEntity);
            await _unitOfWorkApi.ProcessorsRepository.UpdateAsync(bdEntity);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCPU(int id)
        {
            var cpuToDelete = await _unitOfWorkApi.ProcessorsRepository.GetByIdAsync(id);

            if (cpuToDelete == null) return false;

            await _unitOfWorkApi.ProcessorsRepository.DeleteAsync(cpuToDelete);

            return await _unitOfWorkApi.SaveChangesAsync() > 0;
        }
    }
}