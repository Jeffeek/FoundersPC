﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/producers")]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService service) => _producerService = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get() => Ok(await _producerService.GetAllProducersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var producer = await _producerService.GetProducerByIdAsync(id.Value);

            if (producer == null) return NotFound();

            return Ok(producer);
        }
    }
}