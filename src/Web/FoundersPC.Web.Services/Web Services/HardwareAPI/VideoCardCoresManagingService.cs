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
    public class VideoCardCoresManagingService : BoilerplaitManagingService<VideoCardCoreInsertDto, VideoCardCoreReadDto, VideoCardCoreUpdateDto>,
                                                 IVideoCardCoresManagingService
    {
        public VideoCardCoresManagingService(IHttpClientFactory clientFactory,
                                             ILogger<IVideoCardCoresManagingService> logger) : base(clientFactory,
                                                                                                    logger,
                                                                                                    HardwareApiRoutes.VideoCardCoresEndpoint) { }

        /// <inheritdoc/>
        public VideoCardCoresManagingService(IHttpClientFactory clientFactory, ILogger logger, string apiRoute) : base(clientFactory, logger, apiRoute) { }

        #region Implementation of IVideoCardCoresManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore, string managerToken) =>
            UpdateAsync(id, videoCardCore, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteVideoCardCoreAsync(int videoCardCoreId, string managerToken) => DeleteAsync(videoCardCoreId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore, string managerToken) => CreateAsync(videoCardCore, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<VideoCardCoreReadDto>> GetPaginateableVideoCardCoresAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}