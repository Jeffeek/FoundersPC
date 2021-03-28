#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/SolidStateDrives")]
    [Route("HardwareApi/SSDs")]
    public class SolidStateDrivesController : Controller
    {
        private readonly ILogger<SolidStateDrivesController> _logger;
        private readonly IMapper _mapper;
        private readonly ISSDService _ssdService;

        public SolidStateDrivesController(ISSDService ssdService,
                                          IMapper mapper,
                                          ILogger<SolidStateDrivesController> logger)
        {
            _ssdService = ssdService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SSDReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _ssdService.GetAllSSDsAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SSDReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var ssd = await _ssdService.GetSSDByIdAsync(id.Value);

            return ssd == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(ssd);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] SSDUpdateDto ssd)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _ssdService.UpdateSSDAsync(id.Value, ssd);

            return result ? Json(ssd) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] SSDInsertDto ssd)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _ssdService.CreateSSDAsync(ssd);

            return insertResult ? Json(ssd) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readSSD = await _ssdService.GetSSDByIdAsync(id.Value);

            if (readSSD == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _ssdService.DeleteSSDAsync(id.Value);

            return result ? Json(readSSD) : ResponseResultsHelper.DeleteError();
        }
    }
}