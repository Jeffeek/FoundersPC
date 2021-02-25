#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/videocardcores")]
    public class VideoCardCoresController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVideoCardCoreService _videoCardCoreService;

        public VideoCardCoresController(IVideoCardCoreService service,
                                        IMapper mapper
        )
        {
            _videoCardCoreService = service;
            _mapper = mapper;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() => Json(await _videoCardCoreService.GetAllVideoCardCoresAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult("null");

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            return readDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(readDto);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] VideoCardCoreUpdateDto videoCardCore)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var result = await _videoCardCoreService.UpdateVideoCardCore(id.Value, videoCardCore);

            return result ? Json(videoCardCore) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] VideoCardCoreInsertDto videoCardCore)
        {
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var insertResult = await _videoCardCoreService.CreateVideoCardCore(videoCardCore);

            return insertResult ? Json(videoCardCore) : ResultsHelper.InsertError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            if (readDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _videoCardCoreService.DeleteVideoCardCore(id.Value);

            return result ? Json(readDto) : ResultsHelper.DeleteError();
        }
    }
}