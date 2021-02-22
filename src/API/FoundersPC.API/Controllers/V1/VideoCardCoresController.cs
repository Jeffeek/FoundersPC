using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using Microsoft.AspNetCore.Authorization;

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/videocardcores")]
    [Authorize]
    public class VideoCardCoresController : Controller
    {
        private readonly IVideoCardCoreService _videoCardCoreService;
        private readonly IMapper _mapper;

        public VideoCardCoresController(IVideoCardCoreService service,
                                        IMapper mapper)
        {
            _videoCardCoreService = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() =>
            Ok(await _videoCardCoreService.GetAllVideoCardCoresAsync());

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CaseReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            if (readDto == null) return NotFound(nameof(readDto));

            return Ok(readDto);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, VideoCardCoreUpdateDto videoCardCore)
        {
            if (!id.HasValue) return BadRequest(nameof(id));
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var result = await _videoCardCoreService.UpdateVideoCardCore(id.Value, videoCardCore);

            if (!result) return Problem();

            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert(VideoCardCoreInsertDto videoCardCore)
        {
            if (!TryValidateModel(videoCardCore)) return ValidationProblem(ModelState);

            var insertResult = await _videoCardCoreService.CreateVideoCardCore(videoCardCore);

            return !insertResult ? Problem() : Ok(videoCardCore);
        }

        [Authorize(Roles = "Administrator")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readDto = await _videoCardCoreService.GetVideoCardCoreByIdAsync(id.Value);

            if (readDto == null) return NotFound(id);

            var result = await _videoCardCoreService.DeleteVideoCardCore(id.Value);

            return result ? Ok(readDto) : Problem();
        }
    }
}
