#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [ApiController]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [Route(IdentityServerRoutes.Users.Entrances.EntrancesEndpoint)]
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

        [HttpGet(IdentityServerRoutes.Users.Entrances.GetEntrances.All)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> Get() =>
            await _usersEntrancesService
                .GetAllAsync();

        [HttpGet(IdentityServerRoutes.Users.Entrances.GetEntrances.ById)]
        public async ValueTask<UserEntranceLogReadDto> Get([FromRoute] int id) => await _usersEntrancesService.GetByIdAsync(id);

        [HttpGet(IdentityServerRoutes.Users.Entrances.User.ByUserId)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] int id) =>
            await _usersEntrancesService.GetAllUserEntrancesAsync(id);

        [HttpGet(IdentityServerRoutes.Users.Entrances.User.ByUserEmail)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] string email) =>
            await _usersEntrancesService.GetAllUserEntrancesAsync(email);

        [HttpGet(IdentityServerRoutes.Users.Entrances.GetEntrances.InnerEntrancesEndpoint)]
        public async ValueTask<IPaginationResponse<UserEntranceLogReadDto>>
            GetPaginateableUserEntrances([FromQuery(Name = "Page")] int pageNumber = 1,
                                         [FromQuery(Name = "Size")] int pageSize = 10) =>
            await _usersEntrancesService.GetPaginateableAsync(pageNumber, pageSize);

        [HttpGet(IdentityServerRoutes.Users.Entrances.GetEntrances.Between)]
        public async Task<ActionResult<IEnumerable<UserEntranceLogReadDto>>>
            GetUsersEntrancesBetween([FromQuery(Name = "Start")] DateTime start,
                                     [FromQuery(Name = "Finish")] DateTime finish)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entrances = await _usersEntrancesService.GetEntrancesBetweenAsync(start, finish);

            return Ok(entrances ?? Enumerable.Empty<UserEntranceLogReadDto>());
        }
    }
}