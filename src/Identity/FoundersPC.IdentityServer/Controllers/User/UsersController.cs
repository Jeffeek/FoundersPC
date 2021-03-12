using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.IdentityServer.Controllers.User
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
			   Roles = "Administrator")]
	[Route("identityAPI/users")]
	[ApiController]
	public class UsersController : Controller
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService) => _usersService = usersService;

		[HttpGet("{id}")]
		public async Task<ActionResult<UserEntityReadDto>> Get(int? id)
		{
			if (!id.HasValue
				|| id.Value < 1)
				return BadRequest();

			var user = await _usersService.GetByIdAsync(id.Value);

			if (user is null) return NotFound();

			return user;
		}

		[HttpGet]
		public async Task<IEnumerable<UserEntityReadDto>> Get()
		{
			var users = await _usersService.GetAllAsync();

			return users;
		}
	}
}