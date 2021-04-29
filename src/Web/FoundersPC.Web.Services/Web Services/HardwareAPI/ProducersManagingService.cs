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
    public class ProducersManagingService : BoilerplaitManagingService<ProducerInsertDto, ProducerReadDto, ProducerUpdateDto>, IProducersManagingService
    {
        public ProducersManagingService(IHttpClientFactory clientFactory,
                                        ILogger<ProducersManagingService> logger) : base(clientFactory,
                                                                                         logger,
                                                                                         HardwareApiRoutes.ProducersEndpoint) { }

        #region Implementation of IProducersManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync(string managerToken) =>
            GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<ProducerReadDto> GetProducerByIdAsync(int id, string managerToken) =>
            GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer, string managerToken) =>
            UpdateAsync(id, producer, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteProducerAsync(int producerId, string managerToken) =>
            DeleteAsync(producerId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateProducerAsync(ProducerInsertDto producer, string managerToken) =>
            CreateAsync(producer, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<ProducerReadDto>> GetPaginateableProducersAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}