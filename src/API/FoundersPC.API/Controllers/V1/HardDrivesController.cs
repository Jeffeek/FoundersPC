#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
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
    [Route("api/harddrives")]
    [Route("api/hdds")]
    public class HardDrivesController : Controller
    {
        private readonly IHDDService _hddService;
        private readonly IMapper _mapper;

        public HardDrivesController(IHDDService hddService, IMapper mapper)
        {
            _hddService = hddService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HDDReadDto>>> Get() => Json(await _hddService.GetAllHDDsAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<HDDReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var hddReadDto = await _hddService.GetHDDByIdAsync(id.Value);

            return hddReadDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(hddReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] HDDUpdateDto hdd)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(hdd)) return ValidationProblem(ModelState);

            var result = await _hddService.UpdateHDDAsync(id.Value, hdd);

            return result ? Json(hdd) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] HDDInsertDto hdd)
        {
            if (!TryValidateModel(hdd)) return ValidationProblem(ModelState);

            var insertResult = await _hddService.CreateHDDAsync(hdd);

            return insertResult ? Json(hdd) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var hddReadDto = await _hddService.GetHDDByIdAsync(id.Value);

            if (hddReadDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _hddService.DeleteHDDAsync(id.Value);

            return result ? Json(hddReadDto) : ResultsHelper.DeleteError();
        }
    }
}