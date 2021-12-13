#region Using namespaces

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.BackgroundJobsService.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger) => _logger = logger;

    public void OnGet() { }
}