#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/cases")]
    [Authorize]
    public class CasesController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        public CasesController(ICaseService service, IMapper mapper)
        {
            _caseService = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        //[Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() =>
            Ok(await _caseService.GetAllCasesAsync());

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var @case = await _caseService.GetCaseByIdAsync(id.Value);

            if (@case == null) return NotFound(nameof(@case));

            return Ok(@case);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, CaseUpdateDto @case)
        {
            if (!id.HasValue) return BadRequest(nameof(id));
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var result = await _caseService.UpdateCase(id.Value, @case);

            if (!result) return Problem();

            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert(CaseInsertDto @case)
        {
            if (!TryValidateModel(@case)) return ValidationProblem(ModelState);

            var insertResult = await _caseService.CreateCase(@case);

            return !insertResult ? Problem() : Ok(@case);
        }

        [Authorize(Roles = "Administrator")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readCase = await _caseService.GetCaseByIdAsync(id.Value);

            if (readCase == null) return NotFound(id);

            var result = await _caseService.DeleteCase(id.Value);

            return result ? Ok(readCase) : Problem();
        }
    }
}