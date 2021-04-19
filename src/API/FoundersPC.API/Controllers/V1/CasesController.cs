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
    [Route(HardwareApiRoutes.CasesEndpoint)]
    [ModelValidation]
    public class CasesController : Controller
    {
        private readonly ICasesService _casesService;
        private readonly ILogger<CasesController> _logger;

        public CasesController(ICasesService service, ILogger<CasesController> logger)
        {
            _logger = logger;
            _casesService = service;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _casesService.GetAllCasesAsync());
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _casesService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async ValueTask<ActionResult<CaseReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var @case = await _casesService.GetCaseByIdAsync(id);

            return @case == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(@case);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async ValueTask<ActionResult> Update([FromRoute] int id, [FromBody] CaseUpdateDto @case)
        {
            var result = await _casesService.UpdateCaseAsync(id, @case);

            if (!result)
                return ResponseResultsHelper.UpdateError();

            _logger.LogForModelUpdate(HttpContext, id);

            return Json(@case);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async ValueTask<ActionResult> Create([FromBody] CaseInsertDto @case)
        {
            var insertResult = await _casesService.CreateCaseAsync(@case);

            if (!insertResult)
                return ResponseResultsHelper.InsertError();

            _logger.LogForModelInsert(HttpContext);

            return Json(@case);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async ValueTask<ActionResult> Delete([FromRoute] int id)
        {
            var result = await _casesService.DeleteCaseAsync(id);

            if (!result)
                return ResponseResultsHelper.DeleteError();

            _logger.LogForModelDelete(HttpContext, id);

            return Ok();
        }
    }
}