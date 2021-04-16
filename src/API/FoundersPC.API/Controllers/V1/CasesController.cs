#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
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
    [Route(HardwareApiRoutes.Cases)]
    public class CasesController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly ILogger<CasesController> _logger;

        public CasesController(ICaseService service, ILogger<CasesController> logger)
        {
            _logger = logger;
            _caseService = service;
            _logger = logger;
        }

        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _caseService.GetAllCasesAsync());
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _caseService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async ValueTask<ActionResult<CaseReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var @case = await _caseService.GetCaseByIdAsync(id);

            return @case == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async ValueTask<ActionResult> Update([FromRoute] int id, [FromBody] CaseUpdateDto @case)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCaseAsync(id, @case);

            if (!result)
                return ResponseResultsHelper.UpdateError();

            _logger.LogForModelUpdate(HttpContext, id);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async ValueTask<ActionResult> Create([FromBody] CaseInsertDto @case)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCaseAsync(@case);

            if (!insertResult)
                return ResponseResultsHelper.InsertError();

            _logger.LogForModelInsert(HttpContext);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async ValueTask<ActionResult> Delete([FromRoute] int id)
        {
            var readCase = await _caseService.GetCaseByIdAsync(id);

            if (readCase == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _caseService.DeleteCaseAsync(id);

            if (!result)
                return ResponseResultsHelper.DeleteError();

            _logger.LogForModelDelete(HttpContext, id);

            return Json(readCase);
        }
    }
}