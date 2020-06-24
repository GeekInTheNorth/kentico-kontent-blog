using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModelBuilder : BaseViewModelBuilder<ArticleViewModel, BlogArticle>, IArticleViewModelBuilder
    {
        private string articleCodeName;

        private readonly IUrlHelper _urlHelper;

        public ArticleViewModelBuilder(
            IDeliveryClientFactory deliveryClientFactory,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) : 
            base(deliveryClientFactory)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public IArticleViewModelBuilder WithBlogArticle(string articleCodeName)
        {
            this.articleCodeName = articleCodeName;

            return this;
        }

        protected override async Task<ArticleViewModel> BuildModelAsync()
        {
            var article = await GetAsync(articleCodeName);

            return article == null ? null : new ArticleViewModel
            {
                Title = article?.Title,
                Content = article?.ArticleContent,
                HeroImage = article?.HeaderImage?.FirstOrDefault()?.Url,
                PublishedDate = article?.PublishedDate,
                Categories = article?.Categories?.ToDictionary(x => x.Codename, y => y.Name),
                Seo = new SeoMetaData
                {
                    Title = string.IsNullOrWhiteSpace(article.SeoMetaDataSeoTitle) ? article.Title : article.SeoMetaDataSeoTitle,
                    Description = article.SeoMetaDataSeoDescription,
                    Image = article.SeoMetaDataSeoImage?.FirstOrDefault()?.Url ?? article.HeaderImage?.FirstOrDefault()?.Url,
                    ContentType = Globals.Seo.ArticleContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = article.System.Codename }, Globals.Routing.DefaultProtocol),
                    TwitterAuthor = article.SeoMetaDataTwitterAccountName?.Select(x => x.Name).FirstOrDefault() ?? Globals.Seo.TwitterSiteAuthor
                }
            };
        }
    }
}
