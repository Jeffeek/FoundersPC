﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Entrances;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Logs
{
    [Route("FoundersPCIdentity/UsersEntrances")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = ApplicationRoles.Administrator)]
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

        [HttpGet]
        public async Task<IEnumerable<UserEntranceLogReadDto>> Get() =>
            _mapper.Map<IEnumerable<UserEntranceLogReadDto>, IEnumerable<UserEntranceLogReadDto>>(await _usersEntrancesService.GetAllAsync());

        [Route("{id:int}")]
        [HttpGet]
        public async Task<UserEntranceLogReadDto> Get([FromRoute] int id) => await _usersEntrancesService.GetByIdAsync(id);

        [Route("ById/{userId:int}")]
        [HttpGet]
        public async Task<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] int userId) =>
            await _usersEntrancesService.GetAllUserEntrances(userId);

        [Route("ByEmail/{userEmail}")]
        [HttpGet]
        public async Task<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] string userEmail) =>
            await _usersEntrancesService.GetAllUserEntrances(userEmail);

        [HttpGet("Between")]
        public async Task<ActionResult<IEnumerable<UserEntranceLogReadDto>>> GetUsersEntrancesBetween([FromQuery(Name = "Start")] string dateStart,
            [FromQuery(Name = "Finish")] string dateFinish)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!DateTime.TryParseExact(dateStart,
                                        "yyyyMMdd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.AdjustToUniversal,
                                        out var start)
                || !DateTime.TryParseExact(dateFinish,
                                           "yyyyMMdd",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.AdjustToUniversal,
                                           out var finish))
                return BadRequest();

            var entrances = await _usersEntrancesService.GetEntrancesBetweenAsync(start, finish);

            return Ok(entrances ?? Enumerable.Empty<UserEntranceLogReadDto>());
        }
    }
}