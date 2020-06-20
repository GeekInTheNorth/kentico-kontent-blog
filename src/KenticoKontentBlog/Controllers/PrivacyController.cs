using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Privacy;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KenticoKontentBlog.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly ILogger<PrivacyController> _logger;

        private readonly IPrivacyViewModelBuilder _viewModelBuilder;

        public PrivacyController(ILogger<PrivacyController> logger, IPrivacyViewModelBuilder viewModelBuilder)
        {
            _logger = logger;
            _viewModelBuilder = viewModelBuilder;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _viewModelBuilder.BuildAsync();

            return View(model);
        }
    }
}
