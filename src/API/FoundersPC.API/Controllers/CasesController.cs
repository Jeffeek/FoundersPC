﻿#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Mvc;

#endregion

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
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get()
        {
            return Ok(await _caseService.GetAllCasesAsync());
        }
    }
}