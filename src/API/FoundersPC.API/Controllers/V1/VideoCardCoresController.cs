#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/GPUCores")]
    [Route(HardwareApiRoutes.VideoCardCoresEndpoint)]
    [ModelValidation]
    public class VideoCardCoresController : Controller
    {
        private readonly ILogger<VideoCardCoresController> _logger;
        private readonly IVideoCardCoresService _videoCardCoresService;

        public VideoCardCoresController(IVideoCardCoresService service,
                                        ILogger<VideoCardCoresController> logger)
        {
            _videoCardCoresService = service;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _videoCardCoresService.GetAllVideoCardCoresAsync());
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _videoCardCoresService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<CaseReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var readDto = await _videoCardCoresService.GetVideoCardCoreByIdAsync(id);

            return readDto == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(readDto);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] VideoCardCoreUpdateDto videoCardCore)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _videoCardCoresService.UpdateVideoCardCoreAsync(id, videoCardCore);

            return result ? Json(videoCardCore) : ResponseResultsHelper.UpdateError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] VideoCardCoreInsertDto videoCardCore)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _videoCardCoresService.CreateVideoCardCoreAsync(videoCardCore);

            return insertResult ? Json(videoCardCore) : ResponseResultsHelper.InsertError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var result = await _videoCardCoresService.DeleteVideoCardCoreAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}