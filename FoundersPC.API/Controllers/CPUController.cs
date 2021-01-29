#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Core.Hardware_API.Processors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/cpu")]
	public class CPUController : ControllerBase
	{
		private readonly ICPUService _cpuService;

		public CPUController(ICPUService request) => _cpuService = request;

		[HttpGet]
		public async Task<ActionResult> Get() => Ok(await _cpuService.GetAllProducersAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var cpu = await _cpuService.GetProducerByIdAsync(id.Value);
			if (cpu == null) return NotFound();
			return Ok(cpu);
		}
	}
}