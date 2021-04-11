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
    [Route("HardwareApi/Motherboards")]
    public class MotherboardsController : Controller
    {
        private readonly ILogger<MotherboardsController> _logger;
        private readonly IMotherboardService _motherboardService;

        public MotherboardsController(IMotherboardService service,
                                      ILogger<MotherboardsController> logger)
        {
            _motherboardService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotherboardReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _motherboardService.GetAllMotherboardsAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<MotherboardReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id);

            return motherboardReadDto == null
                       ? ResponseResultsHelper.NotFoundByIdResult(id)
                       : Json(motherboardReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] MotherboardUpdateDto motherboard)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _motherboardService.UpdateMotherboardAsync(id, motherboard);

            return result ? Json(motherboard) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] MotherboardInsertDto motherboard)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _motherboardService.CreateMotherboardAsync(motherboard);

            return insertResult ? Json(motherboard) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id);

            if (motherboardReadDto == null) return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _motherboardService.DeleteMotherboardAsync(id);

            return result ? Json(motherboardReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}