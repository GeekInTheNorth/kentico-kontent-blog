using KenticoKontentBlog.Feature.Article;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Controllers
{
    public class ArticleController : Controller
    {
        public readonly IArticleViewModelBuilder _viewModelBuilder;

        public ArticleController(IArticleViewModelBuilder viewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
        }

        [Route("article/{articleStub}")]
        public async Task<IActionResult> IndexAsync(string articleStub)
        {
            var article = await _viewModelBuilder.WithBlogArticle(articleStub).BuildAsync();

            return View(article);
        }
    }
}