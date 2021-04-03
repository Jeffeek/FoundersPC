#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/VideoCardCores")]
    [Route("HardwareApi/GPUCores")]
    public class VideoCardCoresController : Controller
    {
        private readonly ILogger<VideoCardCoresController> _logger;
        private readonly IMapper _mapper;
        private readonly IVideoCardCoreService _videoCardCoreService;

        public VideoCardCoresController(IVideoCardCoreService service,
                                        IMapper mapper,
                                        ILogger<VideoCardCoresController> logger)
        {
            _videoCardCoreService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _videoCardCoreService.GetAllVideoCardCoresAsync());
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult("null");

            _logger.LogForModelRead(HttpContext, id.Value);

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            return readDto == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(readDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] VideoCardCoreUpdateDto videoCardCore)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _videoCardCoreService.UpdateVideoCardCoreAsync(id.Value, videoCardCore);

            return result ? Json(videoCardCore) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] VideoCardCoreInsertDto videoCardCore)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _videoCardCoreService.CreateVideoCardCoreAsync(videoCardCore);

            return insertResult ? Json(videoCardCore) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            if (readDto == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _videoCardCoreService.DeleteVideoCardCoreAsync(id.Value);

            return result ? Json(readDto) : ResponseResultsHelper.DeleteError();
        }
    }
}