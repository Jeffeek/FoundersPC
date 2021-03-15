#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Administration
{
    [Route("identityAPI/admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = "Administrator")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}