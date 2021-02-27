#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Mvc;

#endregion

// todo: Решить проблему валидации пи пустом объекте на входе

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/cases")]
    public class CasesController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        public CasesController(ICaseService service, IMapper mapper)
        {
            _caseService = service;
            _mapper = mapper;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() => Json(await _caseService.GetAllCasesAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var @case = await _caseService.GetCaseByIdAsync(id.Value);

            return @case == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(@case);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] CaseUpdateDto @case)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCaseAsync(id.Value, @case);

            return result ? Json(@case) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CaseInsertDto @case)
        {
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCaseAsync(@case);

            return insertResult ? Json(@case) : ResultsHelper.InsertError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCase = await _caseService.GetCaseByIdAsync(id.Value);

            if (readCase == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _caseService.DeleteCaseAsync(id.Value);

            return result ? Json(readCase) : ResultsHelper.DeleteError();
        }
    }
}