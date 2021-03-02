#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/producers")]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService service) => _producerService = service;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager, DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get() => Json(await _producerService.GetAllProducersAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager, DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var producer = await _producerService.GetProducerByIdAsync(id.Value);

            return producer == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(producer);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] ProducerUpdateDto producer)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var result = await _producerService.UpdateProducerAsync(id.Value, producer);

            return result ? Json(producer) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProducerInsertDto producer)
        {
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var insertResult = await _producerService.CreateProducerAsync(producer);

            return insertResult ? Json(producer) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCase = await _producerService.GetProducerByIdAsync(id.Value);

            if (readCase == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _producerService.DeleteProducerAsync(id.Value);

            return result ? Json(readCase) : ResultsHelper.DeleteError();
        }
    }
}