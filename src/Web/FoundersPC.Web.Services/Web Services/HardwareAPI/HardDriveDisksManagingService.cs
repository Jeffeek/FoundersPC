#region Using namespaces

using System;
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
    public class HardDriveDisksManagingService : BoilerplaitManagingService<HardDriveDiskInsertDto, HardDriveDiskReadDto, HardDriveDiskUpdateDto>,
                                                 IHardDriveDisksManagingService
    {
        public HardDriveDisksManagingService(IHttpClientFactory clientFactory,
                                             ILogger<HardDriveDisksManagingService> logger) : base(clientFactory,
                                                                                                   logger,
                                                                                                   HardwareApiRoutes.HardDriveDisksEndpoint) { }

        #region Implementation of IHardDriveDisksManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<HardDriveDiskReadDto>> GetAllHardDriveDisksAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<HardDriveDiskReadDto> GetHardDriveDiskByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateHardDriveDiskAsync(int id, HardDriveDiskUpdateDto hardDriveDisk, string managerToken) =>
            UpdateAsync(id, hardDriveDisk, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteHardDriveDiskAsync(int hardDriveDiskId, string managerToken) => DeleteAsync(hardDriveDiskId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateHardDriveDiskAsync(HardDriveDiskInsertDto hardDriveDisk, string managerToken) => throw new NotImplementedException();

        /// <inheritdoc/>
        public Task<IPaginationResponse<HardDriveDiskReadDto>> GetPaginateableHardDriveDisksAsync(int pageNumber,
                                                                                                  int pageSize,
                                                                                                  string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}