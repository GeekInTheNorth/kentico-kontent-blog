using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreviewCollection
    {
        public string Title { get; internal set; }

        public List<ArticlePreview> Articles { get; internal set; }

        public bool ShowAuthor { get; internal set; }

        public bool HasContent => Articles?.Any() ?? false;
    }
}
