using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ApiResult()
        {
            var response = await GlobalContext.WeClient.GetAsync("cpus/1");
            var result = await response.Content.ReadFromJsonAsync<CaseReadDto>();

            return View(result);
        }
    }
}