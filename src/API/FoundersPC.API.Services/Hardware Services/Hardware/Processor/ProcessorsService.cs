#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware.Processor
{
    public class ProcessorsService : IProcessorsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public ProcessorsService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProcessorReadDto>> GetAllProcessorsAsync() =>
            _mapper.Map<IEnumerable<Domain.Entities.Hardware.Processor.Processor>, IEnumerable<ProcessorReadDto>>(await
                                                                                                      _unitOfWorkHardwareAPI
                                                                                                          .ProcessorsRepository
                                                                                                          .GetAllAsync());

        /// <inheritdoc/>
        public async Task<ProcessorReadDto> GetProcessorByIdAsync(int cpuId) =>
            _mapper.Map<Domain.Entities.Hardware.Processor.Processor, ProcessorReadDto>(await _unitOfWorkHardwareAPI
                                                                                              .ProcessorsRepository
                                                                                              .GetByIdAsync(cpuId));

        /// <inheritdoc/>
        public async Task<bool> CreateProcessorAsync(ProcessorInsertDto processor)
        {
            var mappedCpu = _mapper.Map<ProcessorInsertDto, Domain.Entities.Hardware.Processor.Processor>(processor);

            var entityAlreadyExists =
                await _unitOfWorkHardwareAPI.ProcessorsRepository.AnyAsync(x => x.Equals(mappedCpu));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.ProcessorsRepository.AddAsync(mappedCpu);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateProcessorAsync(int id, ProcessorUpdateDto processor)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.ProcessorsRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(processor, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProcessorAsync(int id)
        {
            var removeResult = await _unitOfWorkHardwareAPI.ProcessorsRepository.DeleteAsync(id);

            if (!removeResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #region Implementation of IPaginateableService<ProcessorReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<ProcessorReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<Domain.Entities.Hardware.Processor.Processor>, IEnumerable<ProcessorReadDto>>(await _unitOfWorkHardwareAPI.ProcessorsRepository
                                                                                                         .GetPaginateableAsync(pageNumber,
                                                                                                             pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.ProcessorsRepository.CountAsync();

            return new PaginationResponse<ProcessorReadDto>(items, totalItemsCount);
        }

        #endregion
    }
}