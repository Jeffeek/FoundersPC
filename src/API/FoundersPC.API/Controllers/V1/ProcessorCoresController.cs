#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
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
    [Route("HardwareApi/CPUCores")]
    [Route(HardwareApiRoutes.ProcessorCores)]
    [ModelValidation]
    public class ProcessorCoresController : Controller
    {
        private readonly ILogger<ProcessorCoresController> _logger;
        private readonly IProcessorCoresService _processorCoresService;

        public ProcessorCoresController(IProcessorCoresService processorCoresService,
                                        ILogger<ProcessorCoresController> logger)
        {
            _processorCoresService = processorCoresService;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _processorCoresService.GetAllProcessorCoresAsync());
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

            return Json(await _processorCoresService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var cpuCore = await _processorCoresService.GetProcessorCoreByIdAsync(id);

            return cpuCore == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(cpuCore);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] ProcessorCoreInsertDto cpuCore)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _processorCoresService.CreateProcessorCoreAsync(cpuCore);

            return insertResult
                       ? Json(cpuCore)
                       : ResponseResultsHelper.InsertError("Error when tried to insert new cpu core");
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProcessorCoreUpdateDto cpuCore)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _processorCoresService.UpdateProcessorCoreAsync(id, cpuCore);

            return result ? Json(cpuCore) : ResponseResultsHelper.UpdateError();
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

            var result = await _processorCoresService.DeleteProcessorCoreAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}