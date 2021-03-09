#region Using namespaces

using System.Net.Http.Headers;
using System.Threading.Tasks;
using FoundersPC.Web.Services.Web_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ApplicationMicroservices _applicationMicroservices;

		public HomeController(ApplicationMicroservices applicationMicroservices) => _applicationMicroservices = applicationMicroservices;

		[AllowAnonymous]
		public IActionResult Index() => View();

		[AllowAnonymous]
		public IActionResult AccessDenied() => View("AccessDenied");

		[Authorize]
		public async Task<ActionResult> Cases()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("cases");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> HardDrives()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("harddrives");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> Motherboards()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("motherboards");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> PowerSupplies()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("powersupplies");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> ProcessorCores()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("processorcores");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> Processors()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("processors");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> Producers()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("producers");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> RandomAccessMemory()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("randomaccessmemory");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> SolidStateDrives()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("solidstatedrives");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> VideoCardCores()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("videocardcores");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}

		public async Task<IActionResult> VideoCards()
		{
			if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
			{
				return BadRequest(new
								  {
										  error = "No tokens"
								  });
			}

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
												  token);

			var request = await _applicationMicroservices.HardwareApiServer.GetAsync("videocards");

			var response = await request.Content.ReadAsStringAsync();

			_applicationMicroservices.HardwareApiServer.DefaultRequestHeaders.Authorization = null;

			return Ok(response);
		}
	}
}