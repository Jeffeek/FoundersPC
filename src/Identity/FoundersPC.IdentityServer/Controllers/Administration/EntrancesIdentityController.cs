#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Administration
{
    [Route("FoundersPCIdentity/Admin/UserEntrances")]
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
        public async Task<IEnumerable<ApplicationUserEntrance>> Get() =>
            _mapper
                .Map<IEnumerable<UserEntranceLogReadDto>, IEnumerable<ApplicationUserEntrance>
                >(await _usersEntrancesService.GetAllAsync());

        [Route("{id:int}")]
        [HttpGet]
        public async Task<ApplicationUserEntrance> Get(int id) =>
            _mapper.Map<UserEntranceLogReadDto, ApplicationUserEntrance>(await _usersEntrancesService.GetByIdAsync(id));
    }
}