#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Infrastructure.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware.CPU
{
    public class CPUService : ICPUService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public CPUService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CPUReadDto>> GetAllCPUsAsync()
        {
            return _mapper.Map<IEnumerable<Domain.Entities.Hardware.Processor.CPU>, IEnumerable<CPUReadDto>>(await _unitOfWork
                .ProcessorsRepository
                .GetAllAsync());
        }

        /// <inheritdoc />
        public async Task<CPUReadDto> GetCPUByIdAsync(int cpuId)
        {
            return _mapper.Map<Domain.Entities.Hardware.Processor.CPU, CPUReadDto>(await _unitOfWork.ProcessorsRepository
                                                                                                    .GetByIdAsync(cpuId));
        }

        /// <inheritdoc />
        public async Task<bool> CreateCPU(CPUInsertDto cpu)
        {
            var mappedCpu = _mapper.Map<CPUInsertDto, Domain.Entities.Hardware.Processor.CPU>(cpu);

            if (await _unitOfWork.ProcessorsRepository.AnyAsync(mappedCpu)) return false;

            await _unitOfWork.ProcessorsRepository.AddAsync(mappedCpu);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateCPU(int id, CPUUpdateDto cpu)
        {
            var bdEntity = await _unitOfWork.ProcessorsRepository.GetByIdAsync(id);

            if (bdEntity == null) return false;
            _mapper.Map(cpu, bdEntity);
            await _unitOfWork.ProcessorsRepository.UpdateAsync(bdEntity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCPU(int id)
        {
            var cpuToDelete = await _unitOfWork.ProcessorsRepository.GetByIdAsync(id);

            if (cpuToDelete == null) return false;
            await _unitOfWork.ProcessorsRepository.DeleteAsync(cpuToDelete);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}