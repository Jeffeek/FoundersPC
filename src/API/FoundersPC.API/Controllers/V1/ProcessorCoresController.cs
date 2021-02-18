#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/processorcores")]
    [ApiController]
    [Authorize]
    public class ProcessorCoresController : ControllerBase
    {
        private readonly IProcessorCoreService _service;

        public ProcessorCoresController(IProcessorCoreService service) => _service = service;

        // GET: api/<ProcessorCoresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get() => Ok(await _service.GetAllProcessorCoresAsync());
    }
}