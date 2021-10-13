using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModelBuilder : IArticleListViewModelBuilder
    {
        private string _categoryCodeName;

        private readonly IUrlHelper _urlHelper;

        private readonly IContentService _contentService;

        private readonly IArticlePreviewCollectionBuilder _previewCollectionBuilder;

        public ArticleListViewModelBuilder(
            IContentService contentService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IArticlePreviewCollectionBuilder previewCollectionBuilder)
        {
            _contentService = contentService;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            _previewCollectionBuilder = previewCollectionBuilder;
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
                model.Menu = await _contentService.GetCategoryMenuAsync();
            }

            return model;
        }

        private async Task<ArticleListViewModel> BuildModelAsync()
        {
            var listingPage = await _contentService.GetLatestContentAsync<ArticleListPage>(_categoryCodeName);
            var articles = await _contentService.GetListAsync<ArticlePage>();
            var categoryName = await GetCategoryNameAsync();
            var title = "All Articles";

            if (listingPage != null)
            {
                articles = articles?.Where(x => x.Category != null && x.Category.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                title = listingPage.HeroHeader;
            }
            else if (!string.IsNullOrWhiteSpace(_categoryCodeName) && !string.IsNullOrWhiteSpace(categoryName))
            {
                articles = articles?.Where(x => x.Category != null && x.Category.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                title = $"{categoryName} Articles";
            }

            return articles == null ? null : new ArticleListViewModel
            {
                Hero = new HeroModel
                {
                    Title = title,
                    Image = GetHeroImage(listingPage, articles)
                },
                Articles = _previewCollectionBuilder.Build(articles),
                Seo = new SeoMetaData
                {
                    Title = listingPage?.SeoMetaDataMetaTitle ?? categoryName,
                    Description = listingPage?.SeoMetaDataMetaDescription,
                    TwitterAuthor = listingPage?.SeoMetaDataTwitterAccount?.FirstOrDefault()?.Name,
                    ContentType = Globals.Seo.ContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = _categoryCodeName }, Globals.Routing.DefaultProtocol)
                }
            };
        }

        private async Task<string> GetCategoryNameAsync()
        {
            var menu = await _contentService.GetCategoryMenuAsync();

            return menu.Categories.ContainsKey(_categoryCodeName) ? menu.Categories[_categoryCodeName] : null;
        }

        private string GetHeroImage(ArticleListPage articleListPage, IEnumerable<ArticlePage> articles)
        {
            return GetHeroImage(articleListPage) ?? GetHeroImage(articles);
        }

        private string GetHeroImage(ArticleListPage articleListPage)
        {
            return articleListPage?.HeroHeaderImage?.FirstOrDefault()?.Url;
        }

        private string GetHeroImage(IEnumerable<ArticlePage> articles)
        {
            return articles?.Where(x => x.HeroHeaderImage != null && x.HeroHeaderImage.Any())
                            .OrderByDescending(x => x.PublishedDate)
                            .SelectMany(x => x.HeroHeaderImage)
                            .FirstOrDefault()?.Url;
        }
    }
}