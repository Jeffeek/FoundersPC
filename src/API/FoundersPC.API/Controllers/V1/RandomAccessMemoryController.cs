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
    [Route("HardwareApi/RAMs")]
    [Route(HardwareApiRoutes.RandomAccessMemory)]
    [ModelValidation]
    public class RandomAccessMemoryController : Controller
    {
        private readonly ILogger<RandomAccessMemoryController> _logger;
        private readonly IRandomAccessMemoryService _randomAccessMemoryService;

        public RandomAccessMemoryController(IRandomAccessMemoryService randomAccessMemoryService,
                                            ILogger<RandomAccessMemoryController> logger)
        {
            _randomAccessMemoryService = randomAccessMemoryService;
            _logger = logger;
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<RandomAccessMemoryReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _randomAccessMemoryService.GetAllRandomAccessMemoryAsync());
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

            return Json(await _randomAccessMemoryService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<RandomAccessMemoryReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var ram = await _randomAccessMemoryService.GetRandomAccessMemoryByIdAsync(id);

            return ram == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(ram);
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] RandomAccessMemoryUpdateDto randomAccessMemory)
        {
            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _randomAccessMemoryService.UpdateRandomAccessMemoryAsync(id, randomAccessMemory);

            return result ? Json(randomAccessMemory) : ResponseResultsHelper.UpdateError();
        }

        #region Docs

        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     The address family is
        ///     <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6"/> and the address is bad.
        /// </exception>

        #endregion

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Insert([FromBody] RandomAccessMemoryInsertDto randomAccessMemory)
        {
            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _randomAccessMemoryService.CreateRandomAccessMemoryAsync(randomAccessMemory);

            return insertResult ? Json(randomAccessMemory) : ResponseResultsHelper.InsertError();
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

            var result = await _randomAccessMemoryService.DeleteRandomAccessMemoryAsync(id);

            return result ? Ok() : ResponseResultsHelper.DeleteError();
        }
    }
}