using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KenticoKontentBlog.Feature.Framework.Routing
{
    public class ContentUrlHelper : IContentUrlHelper
    {
        private readonly IUrlHelper _urlHelper;

        public ContentUrlHelper(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public string GetListingUrl(string categoryCodeName)
        {
            return _urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = categoryCodeName }, Globals.Routing.DefaultProtocol);
        }

        public string GetUrl(IContentPage contentPage)
        {
            switch (contentPage)
            {
                case HomePage homePage:
                    return _urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, null, Globals.Routing.DefaultProtocol);
                case ArticlePage articlePage:
                    return _urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = articlePage.System.Codename }, Globals.Routing.DefaultProtocol);
                case AuthorPage authorPage:
                    return _urlHelper.Action(Globals.Routing.Index, Globals.Routing.AuthorController, new { authorCodeName = authorPage.System.Codename }, Globals.Routing.DefaultProtocol);
                default:
                    return null;
            }
        }
    }
}