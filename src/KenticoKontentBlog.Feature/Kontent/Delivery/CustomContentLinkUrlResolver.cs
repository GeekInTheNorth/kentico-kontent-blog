using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Globalization;

namespace KenticoKontentBlog.Feature.Kontent.Delivery
{
    public class CustomContentLinkUrlResolver : IContentLinkUrlResolver
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        protected string CurrentCulture => CultureInfo.CurrentUICulture.Name;

        public CustomContentLinkUrlResolver(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        public string ResolveLinkUrl(ContentLink link)
        {
            var urlHelper = GetHelper(_urlHelperFactory, _actionContextAccessor);

            switch (link.ContentTypeCodename)
            {
                case ArticlePage.Codename:
                    return urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = link.Codename }, Globals.Routing.DefaultProtocol);
                case HomePage.Codename:
                    return urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, null, Globals.Routing.DefaultProtocol);
                default:
                    return urlHelper.Action("NotFound", "Errors");
            }
        }

        public string ResolveBrokenLinkUrl()
        {
            var urlHelper = GetHelper(_urlHelperFactory, _actionContextAccessor);
            return urlHelper.Action("NotFound", "Errors");
        }

        private IUrlHelper GetHelper(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
    }
}
