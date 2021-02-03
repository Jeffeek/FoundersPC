using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Domain.Entities.Hardware.Processor;
using FoundersPC.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Hardware_Services
{
	public class ProcessorCoreService : IProcessorCoreService
	{
		private readonly IUnitOfWorkAsync _unitOfWork;
		private readonly IMapper _mapper;

		public ProcessorCoreService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		#region Implementation of IProcessorCoreService

		/// <inheritdoc />
		public async Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync() => _mapper.Map<IEnumerable<ProcessorCore>, IEnumerable<ProcessorCoreReadDto>>(await _unitOfWork.ProcessorCoresRepository.GetAllAsync());

		/// <inheritdoc />
		public Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int cpuId) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateProcessorCore(ProcessorCoreInsertDto cpu) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateProcessorCore(int id, ProcessorCoreUpdateDto cpu) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteProcessorCore(int id) => throw new NotImplementedException();

		#endregion
	}
}
