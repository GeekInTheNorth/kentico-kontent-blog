using System.Threading.Tasks;
using KenticoKontentBlog.Feature.NotFound;
using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class NotFoundController : Controller
    {
        private readonly INotFoundViewModelBuilder _viewModelBuilder;

        public NotFoundController(INotFoundViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _viewModelBuilder.BuildAsync();

            return View(model);
        }
    }
}