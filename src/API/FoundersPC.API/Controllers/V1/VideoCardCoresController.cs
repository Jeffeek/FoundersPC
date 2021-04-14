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
    [Route("HardwareApi/VideoCardCores")]
    [Route("HardwareApi/GPUCores")]
    public class VideoCardCoresController : Controller
    {
        private readonly ILogger<VideoCardCoresController> _logger;
        private readonly IVideoCardCoreService _videoCardCoreService;

        public VideoCardCoresController(IVideoCardCoreService service,
                                        ILogger<VideoCardCoresController> logger)
        {
            _videoCardCoreService = service;
            _logger = logger;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _videoCardCoreService.GetAllVideoCardCoresAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery(Name = "Page")] int pageNumber = 1,
                                                                                  [FromQuery(Name = "Size")] int pageSize = FoundersPCConstants.PageSize)
        {
            _logger.LogForPaginateableModelsRead(HttpContext, pageNumber, pageSize);

            return Json(await _videoCardCoreService.GetPaginateableAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CaseReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id);

            return readDto == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(readDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] VideoCardCoreUpdateDto videoCardCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _videoCardCoreService.UpdateVideoCardCoreAsync(id, videoCardCore);

            return result ? Json(videoCardCore) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] VideoCardCoreInsertDto videoCardCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _videoCardCoreService.CreateVideoCardCoreAsync(videoCardCore);

            return insertResult ? Json(videoCardCore) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id);

            if (readDto == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _videoCardCoreService.DeleteVideoCardCoreAsync(id);

            return result ? Json(readDto) : ResponseResultsHelper.DeleteError();
        }
    }
}