using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/cases")]
	public class CasesController : Controller
	{
		private readonly ICaseService _caseService;
		private readonly IMapper _mapper;

		public CasesController(ICaseService service, IMapper mapper)
		{
			_caseService = service;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() => Ok(await _caseService.GetAllCasesAsync());
	}
}
