#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Response.Authentication;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Administration
{
    [Route("FoundersPCIdentity/Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = "Administrator")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;

        public AdminController(IMapper mapper, IAdminService adminService)
        {
            _mapper = mapper;
            _adminService = adminService;
        }

        [Route("NewManager")]
        public async Task<ActionResult<UserSignUpResponse>> RegisterManager(UserSignUpRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _adminService.RegisterNewManagerAsync(request.Email, request.Password);

            var response = new UserSignUpResponse()
                           {
                               Email = request.Email,
                               Role = ApplicationRoles.Manager.ToString(),
                               IsRegistrationSuccessful = result
                           };

            if (result) return response;

            response.ResponseException = "Not successful registration. Check logs";

            return response;
        }
    }
}