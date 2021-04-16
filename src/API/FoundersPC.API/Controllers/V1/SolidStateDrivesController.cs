#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
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
    public class SolidStateDrivesController : Controller
    {
        private readonly ILogger<SolidStateDrivesController> _logger;
        private readonly ISSDService _ssdService;

        public SolidStateDrivesController(ISSDService ssdService,
                                          ILogger<SolidStateDrivesController> logger)
        {
            _ssdService = ssdService;
            _logger = logger;
        }

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<SSDReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _ssdService.GetAllSSDsAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _ssdService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<SSDReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var ssd = await _ssdService.GetSSDByIdAsync(id);

            return ssd == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(ssd);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] SSDUpdateDto ssd)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _ssdService.UpdateSSDAsync(id, ssd);

            return result ? Json(ssd) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] SSDInsertDto ssd)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _ssdService.CreateSSDAsync(ssd);

            return insertResult ? Json(ssd) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readSSD = await _ssdService.GetSSDByIdAsync(id);

            if (readSSD == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _ssdService.DeleteSSDAsync(id);

            return result ? Json(readSSD) : ResponseResultsHelper.DeleteError();
        }
    }
}