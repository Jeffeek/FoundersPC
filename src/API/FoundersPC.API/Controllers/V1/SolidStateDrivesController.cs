#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/solidstatedrives")]
    [Route("api/ssds")]
    public class SolidStateDrivesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISSDService _ssdService;

        public SolidStateDrivesController(ISSDService ssdService, IMapper mapper)
        {
            _ssdService = ssdService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SSDReadDto>>> Get() => Json(await _ssdService.GetAllSSDsAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SSDReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var ssd = await _ssdService.GetSSDByIdAsync(id.Value);

            return ssd == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(ssd);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] SSDUpdateDto ssd)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(ssd)) return ValidationProblem(ModelState);

            var result = await _ssdService.UpdateSSDAsync(id.Value, ssd);

            return result ? Json(ssd) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] SSDInsertDto ssd)
        {
            if (!TryValidateModel(ssd)) return ValidationProblem(ModelState);

            var insertResult = await _ssdService.CreateSSDAsync(ssd);

            return insertResult ? Json(ssd) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readSSD = await _ssdService.GetSSDByIdAsync(id.Value);

            if (readSSD == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _ssdService.DeleteSSDAsync(id.Value);

            return result ? Json(readSSD) : ResultsHelper.DeleteError();
        }
    }
}