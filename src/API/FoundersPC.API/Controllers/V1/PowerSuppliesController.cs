#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/PowerSupplies")]
    [Route("HardwareApi/PSUs")]
    public class PowerSuppliesController : Controller
    {
        private readonly ILogger<PowerSuppliesController> _logger;
        private readonly IPowerSupplyService _powerSupplyService;

        public PowerSuppliesController(IPowerSupplyService service,
                                       ILogger<PowerSuppliesController> logger)
        {
            _powerSupplyService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PowerSupplyReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _powerSupplyService.GetAllPowerSuppliesAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<PowerSupplyReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id);

            return powerSupplyReadDto == null
                       ? ResponseResultsHelper.NotFoundByIdResult(id)
                       : Json(powerSupplyReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] PowerSupplyUpdateDto powerSupply)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _powerSupplyService.UpdatePowerSupplyAsync(id, powerSupply);

            return result ? Json(powerSupply) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] PowerSupplyInsertDto powerSupply)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _powerSupplyService.CreatePowerSupplyAsync(powerSupply);

            return insertResult ? Json(powerSupply) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id);

            if (powerSupplyReadDto == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _powerSupplyService.DeletePowerSupplyAsync(id);

            return result ? Json(powerSupplyReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}