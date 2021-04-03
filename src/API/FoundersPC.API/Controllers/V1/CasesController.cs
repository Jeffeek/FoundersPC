#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    //[EnableCors(PolicyName = "WebClientPolicy")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/Cases")]
    // todo: add token in parameters to check the input token
    public class CasesController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly ILogger<CasesController> _logger;
        private readonly IMapper _mapper;

        public CasesController(ICaseService service, IMapper mapper, ILogger<CasesController> logger)
        {
            _logger = logger;
            _caseService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _caseService.GetAllCasesAsync());
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var @case = await _caseService.GetCaseByIdAsync(id.Value);

            return @case == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update([FromRoute] int? id, [FromBody] CaseUpdateDto @case)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCaseAsync(id.Value, @case);

            if (!result) return ResponseResultsHelper.UpdateError();

            _logger.LogForModelUpdate(HttpContext, id.Value);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CaseInsertDto @case)
        {
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCaseAsync(@case);

            if (!insertResult) return ResponseResultsHelper.InsertError();

            _logger.LogForModelInsert(HttpContext);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            var readCase = await _caseService.GetCaseByIdAsync(id.Value);

            if (readCase == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _caseService.DeleteCaseAsync(id.Value);

            if (!result) return ResponseResultsHelper.DeleteError();

            _logger.LogForModelDelete(HttpContext, id.Value);

            return Json(readCase);
        }
    }
}