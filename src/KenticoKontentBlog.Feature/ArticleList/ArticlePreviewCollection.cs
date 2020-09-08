using KenticoKontentBlog.Feature.Kontent.Models;

using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreviewCollection
    {
        public ArticlePreviewCollection(IEnumerable<ArticlePage> articles) : this(null, articles)
        {
        }

        public ArticlePreviewCollection(string title, IEnumerable<ArticlePage> articles)
        {
            Title = title;
            Articles = articles?.Select(x => new ArticlePreview(x)).OrderByDescending(x => x.PublishedDate).ToList() ?? new List<ArticlePreview>();
        }

        public string Title { get; private set; }

        public List<ArticlePreview> Articles { get; private set; }

        public bool HasContent => Articles?.Any() ?? false;
    }
}
