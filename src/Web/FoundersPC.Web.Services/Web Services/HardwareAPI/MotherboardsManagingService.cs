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
    public class MotherboardsManagingService : BoilerplaitManagingService<MotherboardInsertDto, MotherboardReadDto, MotherboardUpdateDto>,
                                               IMotherboardsManagingService
    {
        public MotherboardsManagingService(IHttpClientFactory clientFactory,
                                           ILogger<MotherboardsManagingService> logger) : base(clientFactory,
                                                                                               logger,
                                                                                               HardwareApiRoutes.MotherboardsEndpoint) { }

        #region Implementation of IMotherboardsManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<MotherboardReadDto> GetMotherboardByIdAsync(int id, string managerToken) => GetMotherboardByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateMotherboardAsync(int id, MotherboardUpdateDto motherboard, string managerToken) => UpdateAsync(id, motherboard, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteMotherboardAsync(int motherboardId, string managerToken) => DeleteAsync(motherboardId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateMotherboardAsync(MotherboardInsertDto motherboard, string managerToken) => CreateAsync(motherboard, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<MotherboardReadDto>> GetPaginateableMotherboardsAsync(int pageNumber,
                                                                                              int pageSize,
                                                                                              string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}