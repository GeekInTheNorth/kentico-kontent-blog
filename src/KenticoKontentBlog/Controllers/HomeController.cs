using System.Diagnostics;
using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Home;
using KenticoKontentBlog.Models;

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
