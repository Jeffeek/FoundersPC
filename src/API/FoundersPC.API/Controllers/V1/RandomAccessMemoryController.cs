#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/randomaccessmemory")]
    [Route("api/rams")]
    public class RandomAccessMemoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRAMService _ramService;

        public RandomAccessMemoryController(IRAMService ramService, IMapper mapper)
        {
            _ramService = ramService;
            _mapper = mapper;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAMReadDto>>> Get() => Json(await _ramService.GetAllRAMsAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RAMReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var ram = await _ramService.GetRAMByIdAsync(id.Value);

            return ram == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(ram);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] RAMUpdateDto ram)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(ram)) return ValidationProblem(ModelState);

            var result = await _ramService.UpdateRAMAsync(id.Value, ram);

            return result ? Json(ram) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] RAMInsertDto ram)
        {
            if (!TryValidateModel(ram)) return ValidationProblem(ModelState);

            var insertResult = await _ramService.CreateRAMAsync(ram);

            return insertResult ? Json(ram) : ResultsHelper.InsertError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readRAM = await _ramService.GetRAMByIdAsync(id.Value);

            if (readRAM == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _ramService.DeleteRAMAsync(id.Value);

            return result ? Json(readRAM) : ResultsHelper.DeleteError();
        }
    }
}