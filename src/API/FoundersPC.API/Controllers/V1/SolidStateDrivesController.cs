#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
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
    [Route("HardwareApi/SSDs")]
    [Route(HardwareApiRoutes.SolidStateDrives)]
    [ModelValidation]
    public class SolidStateDrivesController : Controller
    {
        private readonly ILogger<SolidStateDrivesController> _logger;
        private readonly ISolidStateDrivesService _solidStateDrivesService;

        public SolidStateDrivesController(ISolidStateDrivesService solidStateDrivesService,
                                          ILogger<SolidStateDrivesController> logger)
        {
            _solidStateDrivesService = solidStateDrivesService;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<SolidStateDriveReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _solidStateDrivesService.GetAllSolidStateDrivesAsync());
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

            return Json(await _solidStateDrivesService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<SolidStateDriveReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var ssd = await _solidStateDrivesService.GetSolidStateDriveByIdAsync(id);

            return ssd == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(ssd);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] SolidStateDriveUpdateDto solidStateDrive)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _solidStateDrivesService.UpdateSolidStateDriveAsync(id, solidStateDrive);

            return result ? Json(solidStateDrive) : ResponseResultsHelper.UpdateError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] SolidStateDriveInsertDto solidStateDrive)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _solidStateDrivesService.CreateSolidStateDriveAsync(solidStateDrive);

            return insertResult ? Json(solidStateDrive) : ResponseResultsHelper.InsertError();
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

            var result = await _solidStateDrivesService.DeleteSolidStateDriveAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}