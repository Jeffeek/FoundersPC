#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Core.Hardware_API.Processors;
using FoundersPC.Services.DTO;
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
		public async Task<ActionResult> Get() => Ok(await _cpuService.GetAllCPUsAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var cpu = await _cpuService.GetCPUByIdAsync(id.Value);
			if (cpu == null) return NotFound();
			return Ok(cpu);
		}

		[HttpPost]
		public async Task<ActionResult> Insert(CPUInsertDto cpu)
		{
			if (!ModelState.IsValid) return Problem(nameof(ModelState));
			var insertResult = await _cpuService.CreateCPU(cpu);
			return !insertResult ? Problem() : Ok(cpu);
		}
	}
}