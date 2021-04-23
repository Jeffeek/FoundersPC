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
    public class PowerSuppliesManagingService : BoilerplaitManagingService
                                                <PowerSupplyInsertDto,
                                                    PowerSupplyReadDto,
                                                    PowerSupplyUpdateDto>,
                                                IPowerSuppliesManagingService
    {
        public PowerSuppliesManagingService(IHttpClientFactory clientFactory,
                                            ILogger<PowerSuppliesManagingService> logger) : base(clientFactory,
                                                                                                 logger,
                                                                                                 HardwareApiRoutes.PowerSuppliesEndpoint) { }

        #region Implementation of IPowerSuppliesManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply, string managerToken) => UpdateAsync(id, powerSupply, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeletePowerSupplyAsync(int powerSupplyId, string managerToken) => DeleteAsync(powerSupplyId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto powerSupply, string managerToken) => CreateAsync(powerSupply, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<PowerSupplyReadDto>> GetPaginateablePowerSuppliesAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}