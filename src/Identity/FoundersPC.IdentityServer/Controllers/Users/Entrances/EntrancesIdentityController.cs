#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users.Entrances
{
    [Route("FoundersPCIdentity/Users")]
    [ApiController]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    public class EntrancesIdentityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsersEntrancesService _usersEntrancesService;

        public EntrancesIdentityController(IMapper mapper,
                                           IUsersEntrancesService usersEntrancesService)
        {
            _mapper = mapper;
            _usersEntrancesService = usersEntrancesService;
        }

        [HttpGet("Entrances")]
        public async Task<IEnumerable<UserEntranceLogReadDto>> Get() =>
            _mapper.Map<IEnumerable<UserEntranceLogReadDto>, IEnumerable<UserEntranceLogReadDto>>(await
                _usersEntrancesService
                    .GetAllAsync());

        [HttpGet("Entrances/{id}")]
        public async Task<UserEntranceLogReadDto> Get([FromRoute] int id) =>
            await _usersEntrancesService.GetByIdAsync(id);

        [HttpGet("ById/{userId}/Entrances")]
        public async Task<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] int userId) =>
            await _usersEntrancesService.GetAllUserEntrances(userId);

        [HttpGet("ByEmail/{userEmail}/Entrances")]
        public async Task<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] string userEmail) =>
            await _usersEntrancesService.GetAllUserEntrances(userEmail);

        [HttpGet("Entrances/Between")]
        public async Task<ActionResult<IEnumerable<UserEntranceLogReadDto>>>
            GetUsersEntrancesBetween([FromQuery(Name = "Start")] DateTime start,
                                     [FromQuery(Name = "Finish")] DateTime finish)
        {
            if (!ModelState.IsValid) return BadRequest();

            var entrances = await _usersEntrancesService.GetEntrancesBetweenAsync(start, finish);

            return Ok(entrances ?? Enumerable.Empty<UserEntranceLogReadDto>());
        }
    }
}