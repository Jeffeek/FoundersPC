#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

//todo: add logger
namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/videocardcores")]
    [Route("api/gpucores")]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() => Json(await _videoCardCoreService.GetAllVideoCardCoresAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult("null");

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            return readDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(readDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] VideoCardCoreUpdateDto videoCardCore)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var result = await _videoCardCoreService.UpdateVideoCardCoreAsync(id.Value, videoCardCore);

            return result ? Json(videoCardCore) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] VideoCardCoreInsertDto videoCardCore)
        {
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var insertResult = await _videoCardCoreService.CreateVideoCardCoreAsync(videoCardCore);

            return insertResult ? Json(videoCardCore) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            if (readDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _videoCardCoreService.DeleteVideoCardCoreAsync(id.Value);

            return result ? Json(readDto) : ResultsHelper.DeleteError();
        }
    }
}