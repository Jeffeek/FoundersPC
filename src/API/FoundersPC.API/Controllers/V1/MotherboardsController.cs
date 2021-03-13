#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

//todo: add logger
namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/motherboards")]
    public class MotherboardsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMotherboardService _motherboardService;
        private readonly ILogger<MotherboardsController> _logger;

        public MotherboardsController(IMotherboardService service, IMapper mapper, ILogger<MotherboardsController> logger)
        {
            _motherboardService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotherboardReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _motherboardService.GetAllMotherboardsAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MotherboardReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id.Value);

            return motherboardReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(motherboardReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] MotherboardUpdateDto motherboard)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _motherboardService.UpdateMotherboardAsync(id.Value, motherboard);

            return result ? Json(motherboard) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] MotherboardInsertDto motherboard)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _motherboardService.CreateMotherboardAsync(motherboard);

            return insertResult ? Json(motherboard) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id.Value);

            if (motherboardReadDto == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _motherboardService.DeleteMotherboardAsync(id.Value);

            return result ? Json(motherboardReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}