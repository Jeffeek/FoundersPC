#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
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
    [Route("HardwareApi/PSUs")]
    [Route(HardwareApiRoutes.PowerSuppliesEndpoint)]
    [ModelValidation]
    public class PowerSuppliesController : Controller
    {
        private readonly ILogger<PowerSuppliesController> _logger;
        private readonly IPowerSuppliesService _powerSuppliesService;

        public PowerSuppliesController(IPowerSuppliesService service,
                                       ILogger<PowerSuppliesController> logger)
        {
            _powerSuppliesService = service;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<PowerSupplyReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _powerSuppliesService.GetAllPowerSuppliesAsync());
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

            return Json(await _powerSuppliesService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<PowerSupplyReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var powerSupplyReadDto = await _powerSuppliesService.GetPowerSupplyByIdAsync(id);

            return powerSupplyReadDto == null
                       ? ResponseResultsHelper.NotFoundByIdResult(id)
                       : Json(powerSupplyReadDto);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] PowerSupplyUpdateDto powerSupply)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _powerSuppliesService.UpdatePowerSupplyAsync(id, powerSupply);

            return result ? Json(powerSupply) : ResponseResultsHelper.UpdateError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] PowerSupplyInsertDto powerSupply)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _powerSuppliesService.CreatePowerSupplyAsync(powerSupply);

            return insertResult ? Json(powerSupply) : ResponseResultsHelper.InsertError();
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

            var result = await _powerSuppliesService.DeletePowerSupplyAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}