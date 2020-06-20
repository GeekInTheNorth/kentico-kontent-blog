using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Home;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KenticoKontentBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHomeViewModelBuilder _homeViewModelBuilder;

        public HomeController(ILogger<HomeController> logger, IHomeViewModelBuilder homeViewModelBuilder)
        {
            _logger = logger;
            _homeViewModelBuilder = homeViewModelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeViewModelBuilder.BuildAsync();

            return View(model);
        }
    }
}
