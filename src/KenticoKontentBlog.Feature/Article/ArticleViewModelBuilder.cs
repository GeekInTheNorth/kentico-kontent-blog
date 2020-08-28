using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModelBuilder : IArticleViewModelBuilder
    {
        private string articleCodeName;

        private readonly IUrlHelper _urlHelper;

        private readonly IContentService _contentService;

        public ArticleViewModelBuilder(
            IContentService contentService, 
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _contentService = contentService;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public IArticleViewModelBuilder WithBlogArticle(string articleCodeName)
        {
            this.articleCodeName = articleCodeName;

            return this;
        }

        public async Task<ArticleViewModel> BuildAsync()
        {
            var model = await BuildModelAsync();

            if (model != null)
            {
                model.Menu = await _contentService.GetCategoryMenuAsync();
            }

            return model;
        }

        private async Task<ArticleViewModel> BuildModelAsync()
        {
            var article = await _contentService.GetContentAsync<ArticlePage>(articleCodeName);

            return article == null ? null : new ArticleViewModel
            {
                Hero = new HeroModel
                {
                    Title = article?.HeroHeader,
                    Image = article?.HeroHeaderImage?.FirstOrDefault()?.Url,
                    PublishedDate = article?.PublishedDate ?? article.System.LastModified,
                    HorizontalAlignment = article?.HeroImageHorizontalAlignment ?? ImageHorizontalAlignment.Centre,
                    VerticalAlignment = article?.HeroImageVerticalAlignment ?? ImageVerticalAlignment.Centre
                },
                Content = article?.ArticleContent,
                Categories = article?.Category?.ToDictionary(x => x.Codename, y => y.Name),
                Seo = new SeoMetaData
                {
                    Title = string.IsNullOrWhiteSpace(article.SeoMetaDataMetaTitle) ? article.HeroHeader : article.SeoMetaDataMetaTitle,
                    Description = article.SeoMetaDataMetaDescription,
                    Image = article.SeoMetaDataMetaImages?.FirstOrDefault()?.Url ?? article.HeroHeaderImage?.FirstOrDefault()?.Url,
                    ContentType = Globals.Seo.ArticleContentType,
                    CanonicalUrl = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = article.System.Codename }, Globals.Routing.DefaultProtocol),
                    TwitterAuthor = article.SeoMetaDataTwitterAccount?.Select(x => x.Name).FirstOrDefault() ?? Globals.Seo.TwitterSiteAuthor
                },
                RelatedArticles = article?.RelatedArticles?.Where(x => x is ArticlePage).Select(x => new ArticlePreview(x as ArticlePage)).OrderByDescending(x => x.PublishedDate).ToList()
            };
        }
    }
}
