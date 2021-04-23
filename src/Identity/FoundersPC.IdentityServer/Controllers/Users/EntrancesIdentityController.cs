#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [ApiController]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [Route(IdentityServerRoutes.Logs.UsersEntrances.UsersEntrancesEndpoint)]
    [ModelValidation]
    public class EntrancesIdentityController : Controller
    {
        private readonly IUsersEntrancesService _usersEntrancesService;

        public EntrancesIdentityController(IUsersEntrancesService usersEntrancesService) => _usersEntrancesService = usersEntrancesService;

        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> Get() =>
            await _usersEntrancesService
                .GetAllAsync();

        [HttpGet(ApplicationRestAddons.GetById)]
        public async ValueTask<UserEntranceLogReadDto> Get([FromRoute] int id) => await _usersEntrancesService.GetByIdAsync(id);

        [HttpGet(IdentityServerRoutes.Logs.UsersEntrances.ByUserId)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] int id) =>
            await _usersEntrancesService.GetAllUserEntrancesAsync(id);

        [HttpGet(IdentityServerRoutes.Logs.UsersEntrances.ByUserEmail)]
        public async ValueTask<IEnumerable<UserEntranceLogReadDto>> GetUserEntrances([FromRoute] string email) =>
            await _usersEntrancesService.GetAllUserEntrancesAsync(email);

        [HttpGet]
        public async ValueTask<IPaginationResponse<UserEntranceLogReadDto>>
            GetPaginateableUserEntrances([FromQuery(Name = "Page")] int pageNumber = 1,
                                         [FromQuery(Name = "Size")] int pageSize = 10) =>
            await _usersEntrancesService.GetPaginateableAsync(pageNumber, pageSize);

        [HttpGet(IdentityServerRoutes.Logs.UsersEntrances.Between)]
        public async Task<ActionResult<IEnumerable<UserEntranceLogReadDto>>>
            GetUsersEntrancesBetween([FromQuery(Name = "Start")] DateTime start,
                                     [FromQuery(Name = "Finish")] DateTime finish)
        {
            var entrances = await _usersEntrancesService.GetEntrancesBetweenAsync(start, finish);

            return Json(entrances ?? Enumerable.Empty<UserEntranceLogReadDto>());
        }
    }
}