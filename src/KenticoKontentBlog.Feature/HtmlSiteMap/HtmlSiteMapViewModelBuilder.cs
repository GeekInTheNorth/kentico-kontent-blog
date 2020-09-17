using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Comparables;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public class HtmlSiteMapViewModelBuilder : IHtmlSiteMapViewModelBuilder
    {
        private readonly IContentService _contentService;

        private readonly IUrlHelper _urlHelper;

        public HtmlSiteMapViewModelBuilder(
            IContentService contentService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _contentService = contentService;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task<HtmlSiteMapViewModel> BuildAsync()
        {
            var articles = await _contentService.GetListAsync<ArticlePage>();
            var authors = await _contentService.GetListAsync<AuthorPage>();
            var menu = await _contentService.GetCategoryMenuAsync();

            return new HtmlSiteMapViewModel
            {
                Hero = new HeroModel { Title = "Site Map" },
                Menu = menu,
                Home = ConvertHomePage(),
                Authors = ConvertAuthors(authors),
                ArticleLists = ConvertArticles(articles).ToList()
            };
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
                    Url = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = x.Codename }, Globals.Routing.DefaultProtocol)
                },
                Children = articles.Where(y => y.Category.Any(z => comparer.Equals(z, x))).OrderBy(y => y.HeroHeader).Select(y => new HtmlSiteMapItemViewModel
                {
                    Title = y.HeroHeader,
                    Url = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = y.System.Codename }, Globals.Routing.DefaultProtocol)
                }).ToList()
            }).ToList();
        }

        private HtmlSiteMapItemCollectionViewModel ConvertAuthors(List<AuthorPage> authorPages)
        {
            return new HtmlSiteMapItemCollectionViewModel
            {
                Parent = new HtmlSiteMapItemViewModel
                {
                    Title = "Authors"
                },
                Children = authorPages.Select(y => new HtmlSiteMapItemViewModel
                {
                    Title = y.HeroHeader,
                    Url = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.AuthorController, new { authorCodeName = y.System.Codename }, Globals.Routing.DefaultProtocol)
                }).ToList()
            };
        }

        private HtmlSiteMapItemViewModel ConvertHomePage()
        {
            return new HtmlSiteMapItemViewModel { Title = "Home", Url = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, null, Globals.Routing.DefaultProtocol) };
        }
    }
}
