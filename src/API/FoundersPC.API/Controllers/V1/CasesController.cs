﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _caseService.GetAllCasesAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CaseReadDto>> Get(int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var @case = await _caseService.GetCaseByIdAsync(id);

            return @case == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CaseUpdateDto @case)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCaseAsync(id, @case);

            if (!result) return ResponseResultsHelper.UpdateError();

            _logger.LogForModelUpdate(HttpContext, id);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CaseInsertDto @case)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCaseAsync(@case);

            if (!insertResult) return ResponseResultsHelper.InsertError();

            _logger.LogForModelInsert(HttpContext);

            return Json(@case);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var readCase = await _caseService.GetCaseByIdAsync(id);

            if (readCase == null) return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _caseService.DeleteCaseAsync(id);

            if (!result) return ResponseResultsHelper.DeleteError();

            _logger.LogForModelDelete(HttpContext, id);

            return Json(readCase);
        }
    }
}