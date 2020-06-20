using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Error;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KenticoKontentBlog.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        private readonly IErrorViewModelBuilder _viewModelBuilder;

        public ErrorController(ILogger<ErrorController> logger, IErrorViewModelBuilder homeViewModelBuilder)
        {
            _logger = logger;
            _viewModelBuilder = homeViewModelBuilder;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            var model = await _viewModelBuilder.BuildAsync();

            return View(model);
        }
    }
}
