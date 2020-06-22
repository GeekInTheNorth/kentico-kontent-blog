using KenticoKontentBlog.Feature.About;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutViewModelBuilder _viewModelBuilder;

        public AboutController(IAboutViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("about/{contentStub}")]
        public async Task<IActionResult> IndexAsync(string contentStub)
        {
            var model = await _viewModelBuilder.WithContentStub(contentStub).BuildAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}