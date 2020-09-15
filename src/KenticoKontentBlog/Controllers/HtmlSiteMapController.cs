using System.Threading.Tasks;

using KenticoKontentBlog.Feature.HtmlSiteMap;

using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class HtmlSiteMapController : Controller
    {
        private readonly IHtmlSiteMapViewModelBuilder _viewModelBuilder;

        public HtmlSiteMapController(IHtmlSiteMapViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("sitemap")]
        public async Task<IActionResult> Index()
        {
            var model = await _viewModelBuilder.BuildAsync();

            return View(model);
        }
    }
}
