using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
