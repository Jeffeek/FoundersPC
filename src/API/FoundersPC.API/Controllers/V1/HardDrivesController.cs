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
    [Route("HardwareApi/HardDrives")]
    [Route("HardwareApi/HDDs")]
    public class HardDrivesController : Controller
    {
        private readonly IHDDService _hddService;
        private readonly ILogger<HardDrivesController> _logger;
        private readonly IMapper _mapper;

        public HardDrivesController(IHDDService hddService, IMapper mapper, ILogger<HardDrivesController> logger)
        {
            _hddService = hddService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HDDReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _hddService.GetAllHDDsAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<HDDReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);
            var hddReadDto = await _hddService.GetHDDByIdAsync(id.Value);

            return hddReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(hddReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] HDDUpdateDto hdd)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _hddService.UpdateHDDAsync(id.Value, hdd);

            return result ? Json(hdd) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] HDDInsertDto hdd)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _hddService.CreateHDDAsync(hdd);

            return insertResult ? Json(hdd) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var hddReadDto = await _hddService.GetHDDByIdAsync(id.Value);

            if (hddReadDto == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _hddService.DeleteHDDAsync(id.Value);

            return result ? Json(hddReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}