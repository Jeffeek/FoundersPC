#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHardwareApiService _hardwareApiService;

        public HomeController(IHardwareApiService hardwareApiService) => _hardwareApiService = hardwareApiService;

        [AllowAnonymous]
        public IActionResult Index() => View();

        [AllowAnonymous]
        public IActionResult AccessDenied() => View("AccessDenied");

        [Authorize]
        public async Task<ActionResult> Cases()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("cases");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> HardDrives()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("harddrives");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Motherboards()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("motherboards");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> PowerSupplies()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("powersupplies");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> ProcessorCores()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("processorcores");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Processors()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("processors");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Producers()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("producers");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> RandomAccessMemory()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("randomaccessmemory");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> SolidStateDrives()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("solidstatedrives");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> VideoCardCores()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("videocardcores");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> VideoCards()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("videocards");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        private async Task<string> MakeRequestAndGetResponseFromApiAsync(string apiModels)
        {
            if (!HttpContext.Request.Cookies.TryGetValue("token", out var token)) return null;

            var result = await _hardwareApiService.GetStringAsync(apiModels, token);

            return result;
        }
    }
}