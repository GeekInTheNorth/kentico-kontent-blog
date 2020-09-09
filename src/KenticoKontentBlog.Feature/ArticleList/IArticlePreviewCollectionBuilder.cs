using System.Collections.Generic;

using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public interface IArticlePreviewCollectionBuilder
    {
        ArticlePreviewCollection Build(string Title, IEnumerable<ArticlePage> articles, bool showAuthor = true);

        ArticlePreviewCollection Build(IEnumerable<ArticlePage> articles);
    }
}
