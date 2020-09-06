using System.Collections.Generic;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Author
{
    public class AuthorViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
