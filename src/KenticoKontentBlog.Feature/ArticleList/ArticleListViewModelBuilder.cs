using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModelBuilder : IArticleListViewModelBuilder
    {
        private string _categoryCodeName;

        private readonly IUrlHelper _urlHelper;

        private readonly IContentService _contentService;

        public ArticleListViewModelBuilder(
            IContentService contentService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _contentService = contentService;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public IArticleListViewModelBuilder WithCategory(string categoryCodeName)
        {
            _categoryCodeName = categoryCodeName;

            return this;
        }

        public async Task<ArticleListViewModel> BuildAsync()
        {
            var model = await BuildModelAsync();

            if (model != null)
            {
                model.Menu = await _contentService.GetCategoriesAsync();
            }

            return model;
        }

        private async Task<ArticleListViewModel> BuildModelAsync()
        {
            var articles = await _contentService.GetListAsync<ArticlePage>();
            var categoryName = await GetCategoryNameAsync();
            var title = "All Articles";

            if (!string.IsNullOrWhiteSpace(_categoryCodeName)  && !string.IsNullOrWhiteSpace(categoryName))
            {
                articles = articles?.Where(x => x.Category != null && x.Category.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                title = $"{categoryName} Articles";
            }

            return articles == null ? null : new ArticleListViewModel
            {
                Hero = new HeroModel { Title = title },
                Articles = articles.Select(x => new ArticlePreview(x)).ToList(),
                Seo = new SeoMetaData
                {
                    Title = categoryName,
                    ContentType = Globals.Seo.ContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = _categoryCodeName }, Globals.Routing.DefaultProtocol)
                }
            };
        }

        private async Task<string> GetCategoryNameAsync()
        {
            var menu = await _contentService.GetCategoriesAsync();

            return menu.Categories.ContainsKey(_categoryCodeName) ? menu.Categories[_categoryCodeName] : null;
        }
    }
}