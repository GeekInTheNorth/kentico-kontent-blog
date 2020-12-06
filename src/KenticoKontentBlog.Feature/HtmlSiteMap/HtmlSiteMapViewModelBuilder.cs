using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework.Builders;
using KenticoKontentBlog.Feature.Framework.Routing;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Comparables;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public class HtmlSiteMapViewModelBuilder : IHtmlSiteMapViewModelBuilder
    {
        private readonly IContentService _contentService;

        private readonly IDefaultBuilder _defaultBuilder;

        private readonly IContentUrlHelper _urlHelper;

        public HtmlSiteMapViewModelBuilder(
            IContentService contentService,
            IDefaultBuilder defaultBuilder,
            IContentUrlHelper urlHelper)
        {
            _contentService = contentService;
            _defaultBuilder = defaultBuilder;
            _urlHelper = urlHelper;
        }

        public async Task<HtmlSiteMapViewModel> BuildAsync()
        {
            var htmlSiteMap = await _contentService.GetLatestContentAsync<HtmlSiteMapPage>();
            var articles = await _contentService.GetListAsync<ArticlePage>();
            var authors = await _contentService.GetListAsync<AuthorPage>();
            var home = await _contentService.GetLatestContentAsync<HomePage>();

            var model = new HtmlSiteMapViewModel
            {
                SitePages = ConvertAuthors(home, authors),
                ArticleLists = ConvertArticles(articles).ToList()
            };

            _defaultBuilder.WithContent(htmlSiteMap).WithModel(model).Build();

            return model;
        }

        private List<HtmlSiteMapItemCollectionViewModel> ConvertArticles(List<ArticlePage> articles)
        {
            var comparer = new TaxonomyTermComparer();
            var categories = articles.SelectMany(x => x.Category).Distinct(comparer).OrderBy(x => x.Name);

            return categories.Select(x => new HtmlSiteMapItemCollectionViewModel
            {
                Parent = new HtmlSiteMapItemViewModel
                {
                    Title = x.Name,
                    Url = _urlHelper.GetListingUrl(x.Codename)
                },
                Children = articles.Where(y => y.Category.Any(z => comparer.Equals(z, x))).OrderBy(y => y.HeroHeader).Select(y => new HtmlSiteMapItemViewModel
                {
                    Title = y.HeroHeader,
                    Url = _urlHelper.GetUrl(y)
                }).ToList()
            }).ToList();
        }

        private HtmlSiteMapItemCollectionViewModel ConvertAuthors(HomePage homePage, List<AuthorPage> authorPages)
        {
            return new HtmlSiteMapItemCollectionViewModel
            {
                Parent = new HtmlSiteMapItemViewModel
                {
                    Title = homePage.HeroHeader,
                    Url = _urlHelper.GetUrl(homePage)
                },
                Children = authorPages.Select(y => new HtmlSiteMapItemViewModel
                {
                    Title = y.HeroHeader,
                    Url = _urlHelper.GetUrl(y)
                }).ToList()
            };
        }
    }
}
