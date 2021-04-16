#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/PSUs")]
    [Route(HardwareApiRoutes.PowerSupplies)]
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

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<PowerSupplyReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _powerSupplyService.GetAllPowerSuppliesAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _powerSupplyService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<PowerSupplyReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id);

            return powerSupplyReadDto == null
                       ? ResponseResultsHelper.NotFoundByIdResult(id)
                       : Json(powerSupplyReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] PowerSupplyUpdateDto powerSupply)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _powerSupplyService.UpdatePowerSupplyAsync(id, powerSupply);

            return result ? Json(powerSupply) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] PowerSupplyInsertDto powerSupply)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _powerSupplyService.CreatePowerSupplyAsync(powerSupply);

            return insertResult ? Json(powerSupply) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
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