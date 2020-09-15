using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
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

            return new HtmlSiteMapViewModel
            {
                Home = ConvertHomePage(),
                Authors = ConvertAuthors(authors),
                ArticleLists = ConvertArticles(articles).ToList()
            };
        }

        private List<HtmlSiteMapItemCollectionViewModel> ConvertArticles(List<ArticlePage> articles)
        {
            var categories = articles.SelectMany(x => x.Category).Distinct();

            return categories.Select(x => new HtmlSiteMapItemCollectionViewModel
            {
                Parent = new HtmlSiteMapItemViewModel
                {
                    Title = x.Name,
                    Url = _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = x.Codename }, Globals.Routing.DefaultProtocol)
                },
                Children = articles.Where(y => y.Category.Equals(x)).Select(y => new HtmlSiteMapItemViewModel
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
