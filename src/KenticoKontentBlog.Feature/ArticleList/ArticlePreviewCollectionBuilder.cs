using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreviewCollectionBuilder : IArticlePreviewCollectionBuilder
    {
        private readonly IUrlHelper _urlHelper;

        public ArticlePreviewCollectionBuilder(
            IUrlHelperFactory urlHelperFactory, 
            IActionContextAccessor actionContextAccessor)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public ArticlePreviewCollection Build(IEnumerable<ArticlePage> articles)
        {
            return Build(null, articles, true);
        }

        public ArticlePreviewCollection Build(string title, IEnumerable<ArticlePage> articles, bool showAuthor = true)
        {
            return new ArticlePreviewCollection
            {
                Title = title,
                Articles = articles?.Select(ToArticlePreview).OrderByDescending(x => x.PublishedDate).ToList() ?? new List<ArticlePreview>(),
                ShowAuthor = showAuthor
            };
        }


        private ArticlePreview ToArticlePreview(ArticlePage article)
        {
            var articlePreview = new ArticlePreview
            {
                Title = article.HeroHeader,
                Description = article.SeoMetaDataMetaDescription,
                Image = article.HeroHeaderImage?.FirstOrDefault()?.Url,
                CodeName = article.System.Codename,
                PublishedDate = article.PublishedDate ?? article.System.LastModified
            };
            
            var author = article.Author?.Cast<AuthorPage>().FirstOrDefault();
            if (author != null)
            {
                articlePreview.AuthorName = author.Name;
                articlePreview.AuthorUrl = _urlHelper.Action(Globals.Routing.Index, Globals.Routing.AuthorController, new { authorCodeName = author.System.Codename }, Globals.Routing.DefaultProtocol);
            }
            else
            {
                articlePreview.AuthorName = article.SeoMetaDataTwitterAccount?.FirstOrDefault()?.Name;
                articlePreview.AuthorUrl = $"https://twitter.com/{articlePreview.AuthorName}";
            }

            return articlePreview;
        }
    }
}
