using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.IdentityServer.Controllers.Administration
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
			   Roles = "Administrator")]
	[ApiController]
	public class AdminController : Controller { }
}