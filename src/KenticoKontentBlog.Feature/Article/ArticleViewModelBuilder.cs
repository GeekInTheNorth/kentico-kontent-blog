using System.Linq;
using System.Threading.Tasks;

using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModelBuilder : BaseViewModelBuilder<ArticleViewModel, ArticlePage>, IArticleViewModelBuilder
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
                Title = article?.HeroHeader,
                Content = article?.ArticleContent,
                HeroImage = article?.HeroHeaderImage?.FirstOrDefault()?.Url,
                PublishedDate = article?.PublishedDate ?? article.System.LastModified,
                Categories = article?.Category?.ToDictionary(x => x.Codename, y => y.Name),
                Seo = new SeoMetaData
                {
                    Title = string.IsNullOrWhiteSpace(article.SeoMetaDataMetaTitle) ? article.HeroHeader : article.SeoMetaDataMetaTitle,
                    Description = article.SeoMetaDataMetaDescription,
                    Image = article.SeoMetaDataMetaImages?.FirstOrDefault()?.Url ?? article.HeroHeaderImage?.FirstOrDefault()?.Url,
                    ContentType = Globals.Seo.ArticleContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = article.System.Codename }, Globals.Routing.DefaultProtocol),
                    TwitterAuthor = article.SeoMetaDataTwitterAccount?.Select(x => x.Name).FirstOrDefault() ?? Globals.Seo.TwitterSiteAuthor
                }
            };
        }
    }
}
