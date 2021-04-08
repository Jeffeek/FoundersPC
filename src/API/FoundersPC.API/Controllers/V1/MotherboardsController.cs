#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/Motherboards")]
    public class MotherboardsController : Controller
    {
        private readonly ILogger<MotherboardsController> _logger;
        private readonly IMapper _mapper;
        private readonly IMotherboardService _motherboardService;

        public MotherboardsController(IMotherboardService service,
                                      IMapper mapper,
                                      ILogger<MotherboardsController> logger)
        {
            _motherboardService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotherboardReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _motherboardService.GetAllMotherboardsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotherboardReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id.Value);

            return motherboardReadDto == null
                       ? ResponseResultsHelper.NotFoundByIdResult(id.Value)
                       : Json(motherboardReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody] MotherboardUpdateDto motherboard)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _motherboardService.UpdateMotherboardAsync(id.Value, motherboard);

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