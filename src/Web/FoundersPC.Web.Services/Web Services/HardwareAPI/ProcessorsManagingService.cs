#region Using namespaces

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class ProcessorsManagingService : BoilerplaitManagingService<ProcessorInsertDto, ProcessorReadDto, ProcessorUpdateDto>, IProcessorsManagingService
    {
        public ProcessorsManagingService(IHttpClientFactory clientFactory,
                                         ILogger<ProcessorsManagingService> logger) : base(clientFactory,
                                                                                           logger,
                                                                                           HardwareApiRoutes.ProcessorsEndpoint) { }

        #region Implementation of IProcessorsManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<ProcessorReadDto>> GetAllProcessorsAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<ProcessorReadDto> GetProcessorByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateProcessorAsync(int id, ProcessorUpdateDto processor, string managerToken) => UpdateAsync(id, processor, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteProcessorAsync(int processorId, string managerToken) => DeleteAsync(processorId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateProcessorAsync(ProcessorInsertDto processor, string managerToken) => CreateAsync(processor, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<ProcessorReadDto>> GetPaginateableProcessorsAsync(int pageNumber,
                                                                                          int pageSize,
                                                                                          string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}