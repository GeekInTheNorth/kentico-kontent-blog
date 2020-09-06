using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Author;
using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorViewModelBuilder _viewModelBuilder;

        public AuthorController(IAuthorViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("author/{authorCodeName}")]
        public async Task<IActionResult> Index(string authorCodeName)
        {
            var model = await _viewModelBuilder.WithAuthorCodeName(authorCodeName).BuildAsync();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
