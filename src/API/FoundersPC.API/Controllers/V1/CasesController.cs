﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
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

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> Get() => Ok(await _caseService.GetAllCasesAsync());
    }
}