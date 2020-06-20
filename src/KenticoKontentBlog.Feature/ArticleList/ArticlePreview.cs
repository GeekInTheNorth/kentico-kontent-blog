using KenticoKontentBlog.Feature.Kontent.Models;
using System;
using System.Linq;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreview
    {
        public ArticlePreview(BlogArticle blogArticle)
        {
            Title = blogArticle.Title;
            Description = blogArticle.SeoMetaDataSeoDescription;
            Image = blogArticle.HeaderImage?.FirstOrDefault()?.Url;
            CodeName = blogArticle.System.Codename;
            PublishedDate = blogArticle.PublishedDate ?? blogArticle.System.LastModified;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CodeName { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
