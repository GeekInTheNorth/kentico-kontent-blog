using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Error;

using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IErrorViewModelBuilder _viewModelBuilder;

        public ErrorController(IErrorViewModelBuilder homeViewModelBuilder)
        {
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
