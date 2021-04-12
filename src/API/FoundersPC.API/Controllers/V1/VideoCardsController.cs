#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
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
    [Route("HardwareApi/VideoCards")]
    [Route("HardwareApi/GPUs")]
    public class VideoCardsController : Controller
    {
        private readonly IGPUService _gpuService;
        private readonly ILogger<VideoCardsController> _logger;

        public VideoCardsController(IGPUService service, ILogger<VideoCardsController> logger)
        {
            _gpuService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPUReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _gpuService.GetAllGPUsAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<GPUReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id);

            return gpuReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(gpuReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] GPUUpdateDto gpu)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _gpuService.UpdateGPUAsync(id, gpu);

            return result ? Json(gpu) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] GPUInsertDto gpu)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _gpuService.CreateGPUAsync(gpu);

            return insertResult ? Json(gpu) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id);

            if (gpuReadDto == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _gpuService.DeleteGPUAsync(id);

            return result ? Json(gpuReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}