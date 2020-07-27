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
        [Route("error/{statusCode}")]
        public async Task<IActionResult> Index(int statusCode = 500)
        {
            var model = await _viewModelBuilder.WithStatusCode(statusCode).BuildAsync();

            return View(model);
        }
    }
}
