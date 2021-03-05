#region Using namespaces

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    //[EnableCors(PolicyName = "WebClientPolicy")]
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/cases")]
    public class CasesController : Controller
    {
        private readonly ILogger<CasesController> _logger;
        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        public CasesController(ICaseService service, IMapper mapper, ILogger<CasesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _caseService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _caseService.GetAllCasesAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var @case = await _caseService.GetCaseByIdAsync(id.Value);

            return @case == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(@case);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] CaseUpdateDto @case)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCaseAsync(id.Value, @case);

            if (!result) return ResultsHelper.UpdateError();

            _logger.LogForModelUpdated(HttpContext, id.Value);

            return Json(@case);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CaseInsertDto @case)
        {
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCaseAsync(@case);

            if (!insertResult) return ResultsHelper.InsertError();

            _logger.LogForModelInserted(HttpContext);

            return Json(@case);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCase = await _caseService.GetCaseByIdAsync(id.Value);

            if (readCase == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _caseService.DeleteCaseAsync(id.Value);

            if (!result) return ResultsHelper.DeleteError();

            _logger.LogForModelDeleted(HttpContext, id.Value);

            return Json(readCase);
        }
    }
}