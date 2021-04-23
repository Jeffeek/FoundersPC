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
    public class VideoCardsManagingService : BoilerplaitManagingService<VideoCardInsertDto, VideoCardReadDto, VideoCardUpdateDto>, IVideoCardsManagingService
    {
        public VideoCardsManagingService(IHttpClientFactory clientFactory,
                                         ILogger<IVideoCardsManagingService> logger) : base(clientFactory,
                                                                                            logger,
                                                                                            HardwareApiRoutes.VideoCardsEndpoint) { }

        /// <inheritdoc/>
        public VideoCardsManagingService(IHttpClientFactory clientFactory, ILogger logger, string apiRoute) : base(clientFactory, logger, apiRoute) { }

        #region Implementation of IVideoCardsManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<VideoCardReadDto> GetVideoCardByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard, string managerToken) => UpdateAsync(id, videoCard, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteVideoCardAsync(int videoCardId, string managerToken) => DeleteAsync(videoCardId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard, string managerToken) => CreateAsync(videoCard, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<VideoCardReadDto>> GetPaginateableVideoCardsAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}