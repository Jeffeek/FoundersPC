using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Services.Models;
using FoundersPC.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/producers")]
	public class ProducersController : ControllerBase
    {
	    private readonly IProducersRepository _producersRepository;

	    public ProducersController(IProducersRepository producersRepository)
	    {
		    _producersRepository = producersRepository;
	    }

	    [HttpGet]
	    public async Task<ActionResult<IEnumerable<Producer>>> Get() => Ok(await _producersRepository.GetAllAsync());

	    [HttpGet("{id}")]
	    public async Task<ActionResult<Producer>> Get(int? id)
	    {
		    if (id.HasValue)
			    return Ok(await _producersRepository.GetAsync(id.Value));

		    return NotFound();
	    }
	}
}
