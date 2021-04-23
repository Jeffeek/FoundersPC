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
    [ApiController]
    [Route("HardwareApi/HDDs")]
    [Route(HardwareApiRoutes.HardDriveDisksEndpoint)]
    [ModelValidation]
    public class HardDriveDisksController : Controller
    {
        private readonly IHardDriveDisksService _hardDriveDisksService;
        private readonly ILogger<HardDriveDisksController> _logger;

        public HardDriveDisksController(IHardDriveDisksService hardDriveDisksService, ILogger<HardDriveDisksController> logger)
        {
            _hardDriveDisksService = hardDriveDisksService;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<HardDriveDiskReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _hardDriveDisksService.GetAllHardDiskDrivesAsync());
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

            return Json(await _hardDriveDisksService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<HardDriveDiskReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);
            var hddReadDto = await _hardDriveDisksService.GetHardDiskDriveByIdAsync(id);

            return hddReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(hddReadDto);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] HardDriveDiskUpdateDto hardDriveDisk)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _hardDriveDisksService.UpdateHardDriveDiskAsync(id, hardDriveDisk);

            return result ? Json(hardDriveDisk) : ResponseResultsHelper.UpdateError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] HardDriveDiskInsertDto hardDriveDisk)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _hardDriveDisksService.CreateHardDriveDiskAsync(hardDriveDisk);

            return insertResult ? Json(hardDriveDisk) : ResponseResultsHelper.InsertError();
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

            var result = await _hardDriveDisksService.DeleteHardDriveDiskAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}