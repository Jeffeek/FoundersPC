using System.Collections.Generic;
using FoundersPC.Services.Models;
using FoundersPC.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/cpu")]
    public class CPUController : ControllerBase
	{
		private ICPURepository _cpuRepository;

	    public CPUController(ICPURepository cpuRepository)
	    {
		    _cpuRepository = cpuRepository;
	    }

	    [HttpGet]
	    public ActionResult<IEnumerable<CPU>> Get() => Ok(_cpuRepository.GetAllAsync());

	    [HttpGet("{id}")]
	    public ActionResult<CPU> Get(int? id)
	    {
		    if (id.HasValue)
			    return Ok(_cpuRepository.GetAsync(id.Value));

			return NotFound();
	    }
	}
}
