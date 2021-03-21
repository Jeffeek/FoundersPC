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

        [Authorize]
        public async Task<ActionResult> Cases()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("Cases");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> HardDrives()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("HardDrives");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Motherboards()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("Motherboards");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> PowerSupplies()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("PowerSupplies");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> ProcessorCores()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("ProcessorCores");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Processors()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("Processors");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> Producers()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("Producers");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> RandomAccessMemory()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("RandomAccessMemory");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> SolidStateDrives()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("SolidStateDrives");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> VideoCardCores()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("VideoCardCores");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        public async Task<IActionResult> VideoCards()
        {
            var apiResult = await MakeRequestAndGetResponseFromApiAsync("VideoCards");

            if (apiResult is null) return BadRequest();

            return Ok(apiResult);
        }

        private async Task<string> MakeRequestAndGetResponseFromApiAsync(string apiModels)
        {
            if (!HttpContext.Request.Cookies.TryGetValue("token", out var token)) return null;

            var result = await _hardwareApiService.GetStringAsync(apiModels, token);

            return result;
        }

        public ActionResult SpaceInvaders() => View("Space-Invaders/SpaceInvaders");
    }
}