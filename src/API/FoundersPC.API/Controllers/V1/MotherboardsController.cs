#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
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
    [Route("api/motherboards")]
    public class MotherboardsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMotherboardService _motherboardService;

        public MotherboardsController(IMotherboardService service, IMapper mapper)
        {
            _motherboardService = service;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotherboardReadDto>>> Get() => Json(await _motherboardService.GetAllMotherboardsAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MotherboardReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id.Value);

            return motherboardReadDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(motherboardReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] MotherboardUpdateDto motherboard)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(motherboard)) return ValidationProblem(ModelState);

            var result = await _motherboardService.UpdateMotherboardAsync(id.Value, motherboard);

            return result ? Json(motherboard) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] MotherboardInsertDto motherboard)
        {
            if (!TryValidateModel(motherboard)) return ValidationProblem(ModelState);

            var insertResult = await _motherboardService.CreateMotherboardAsync(motherboard);

            return insertResult ? Json(motherboard) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var motherboardReadDto = await _motherboardService.GetMotherboardByIdAsync(id.Value);

            if (motherboardReadDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _motherboardService.DeleteMotherboardAsync(id.Value);

            return result ? Json(motherboardReadDto) : ResultsHelper.DeleteError();
        }
    }
}