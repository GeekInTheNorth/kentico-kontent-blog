using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public ArticlePreviewCollection Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
