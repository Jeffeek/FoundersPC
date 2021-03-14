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

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/powersupplies")]
    [Route("api/psus")]
    public class PowerSuppliesController : Controller
    {
        private readonly ILogger<PowerSuppliesController> _logger;
        private readonly IMapper _mapper;
        private readonly IPowerSupplyService _powerSupplyService;

        public PowerSuppliesController(IPowerSupplyService service, IMapper mapper, ILogger<PowerSuppliesController> logger)
        {
            _powerSupplyService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PowerSupplyReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _powerSupplyService.GetAllPowerSuppliesAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PowerSupplyReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id.Value);

            return powerSupplyReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(powerSupplyReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] PowerSupplyUpdateDto powerSupply)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _powerSupplyService.UpdatePowerSupplyAsync(id.Value, powerSupply);

            return result ? Json(powerSupply) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] PowerSupplyInsertDto powerSupply)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _powerSupplyService.CreatePowerSupplyAsync(powerSupply);

            return insertResult ? Json(powerSupply) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id.Value);

            if (powerSupplyReadDto == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _powerSupplyService.DeletePowerSupplyAsync(id.Value);

            return result ? Json(powerSupplyReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}