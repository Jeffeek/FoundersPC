using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
	public class ProcessorCoresManagingService : BoilerplaitManagingService<ProcessorCoreInsertDto, ProcessorCoreReadDto, ProcessorCoreUpdateDto>, IProcessorCoresManagingService
	{
		public ProcessorCoresManagingService(IHttpClientFactory clientFactory,
		                                     ILogger<ProcessorCoresManagingService> logger) : base(clientFactory,
		                                                                                           logger,
		                                                                                           HardwareApiRoutes.ProcessorCoresEndpoint) { }

		#region Implementation of IProcessorCoresManagingService

		/// <inheritdoc />
		public Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync(string managerToken) =>
			GetAllAsync(managerToken);

		/// <inheritdoc />
		public Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int id, string managerToken) =>
			GetByIdAsync(id, managerToken);

		/// <inheritdoc />
		public Task<bool> UpdateProcessorCoreAsync(int id, ProcessorCoreUpdateDto processorCore, string managerToken) =>
			UpdateAsync(id, processorCore, managerToken);

		/// <inheritdoc />
		public Task<bool> DeleteProcessorCoreAsync(int processorCoreId, string managerToken) =>
			DeleteAsync(processorCoreId, managerToken);

		/// <inheritdoc />
		public Task<bool> CreateProcessorCoreAsync(ProcessorCoreInsertDto processorCore, string managerToken) =>
			CreateAsync(processorCore, managerToken);

		/// <inheritdoc />
		public Task<IPaginationResponse<ProcessorCoreReadDto>> GetPaginateableProcessorCoresAsync(int pageNumber,
		                                                                                          int pageSize,
		                                                                                          string managerToken) =>
			GetPaginateableAsync(pageNumber, pageSize, managerToken);

		#endregion
	}
}
