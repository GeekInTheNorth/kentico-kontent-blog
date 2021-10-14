using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

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
            var categoryMenu = await _contentService.GetCategoryMenuAsync();
            var articles = await _contentService.GetListAsync<ArticlePage>();

            var listingPages = await _contentService.GetListAsync<ArticleListPage>();
            var listingPage = listingPages.FirstOrDefault(x => x.Category.Any(y => y.Codename.Equals(_categoryCodeName)));

            return listingPage == null ? Build(categoryMenu, articles) : Build(categoryMenu, listingPage, articles);
        }

        private ArticleListViewModel Build(
            Menu categoryMenu,
            ArticleListPage listingPage,
            List<ArticlePage> articles)
        {
            var categoryName = GetCategoryName(categoryMenu);
            var filteredArticles = articles?.Where(x => x.Category != null && x.Category.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();

            return filteredArticles == null ? null : new ArticleListViewModel
            {
                Hero = new HeroModel
                {
                    Title = listingPage.HeroHeader,
                    Image = GetHeroImage(listingPage),
                    HorizontalAlignment = listingPage.HeroImageHorizontalAlignment,
                    VerticalAlignment = listingPage.HeroImageVerticalAlignment,
                    TextColour = listingPage.HeroHeaderTextColour
                },
                Articles = _previewCollectionBuilder.Build(filteredArticles),
                Seo = new SeoMetaData
                {
                    Title = listingPage.SeoMetaDataMetaTitle ?? categoryName,
                    Description = listingPage.SeoMetaDataMetaDescription,
                    TwitterAuthor = listingPage.SeoMetaDataTwitterAccount.FirstOrDefault()?.Name,
                    ContentType = Globals.Seo.ContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = _categoryCodeName }, Globals.Routing.DefaultProtocol)
                },
                Menu = categoryMenu
            };
        }

        private ArticleListViewModel Build(
            Menu categoryMenu,
            List<ArticlePage> articles)
        {
            var title = "All Articles";
            var categoryName = GetCategoryName(categoryMenu);

            if (!string.IsNullOrWhiteSpace(_categoryCodeName) && !string.IsNullOrWhiteSpace(categoryName))
            {
                articles = articles?.Where(x => x.Category != null && x.Category.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                title = $"{categoryName} Articles";
            }

            var latestArticle = GetLatestArticleWithHero(articles);

            return articles == null ? null : new ArticleListViewModel
            {
                Hero = new HeroModel
                {
                    Title = title,
                    Image = GetHeroImage(latestArticle),
                    HorizontalAlignment = latestArticle?.HeroImageHorizontalAlignment ?? ImageHorizontalAlignment.Centre,
                    VerticalAlignment = latestArticle?.HeroImageVerticalAlignment ?? ImageVerticalAlignment.Centre,
                    TextColour = latestArticle?.HeroHeaderTextColour ?? HeaderTextColour.Dark
                },
                Articles = _previewCollectionBuilder.Build(articles),
                Seo = new SeoMetaData
                {
                    Title = categoryName,
                    ContentType = Globals.Seo.ContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = _categoryCodeName }, Globals.Routing.DefaultProtocol)
                },
                Menu = categoryMenu
            };
        }

        private string GetCategoryName(Menu categoryMenu)
        {
            return categoryMenu.Categories.ContainsKey(_categoryCodeName) ? categoryMenu.Categories[_categoryCodeName] : null;
        }

        private string GetHeroImage(ArticleListPage articleListPage)
        {
            return articleListPage?.HeroHeaderImage?.FirstOrDefault()?.Url;
        }

        private string GetHeroImage(ArticlePage articlePage)
        {
            return articlePage?.HeroHeaderImage?.FirstOrDefault()?.Url;
        }

        private ArticlePage GetLatestArticleWithHero(IEnumerable<ArticlePage> articles)
        {
            return articles?.Where(x => x.HeroHeaderImage != null && x.HeroHeaderImage.Any())
                            .OrderByDescending(x => x.PublishedDate)
                            .FirstOrDefault();
        }
    }
}