using KenticoKontentBlog.Feature.Kontent.Models;
using System;
using System.Linq;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreview
    {
        public ArticlePreview(ArticlePage article)
        {
            Title = article.HeroHeader;
            Description = article.SeoMetaDataMetaDescription;
            Image = article.HeroHeaderImage?.FirstOrDefault()?.Url;
            CodeName = article.System.Codename;
            PublishedDate = article.PublishedDate ?? article.System.LastModified;
            AuthorTwitter = article.SeoMetaDataTwitterAccount?.FirstOrDefault()?.Name;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CodeName { get; set; }

        public string AuthorTwitter { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
