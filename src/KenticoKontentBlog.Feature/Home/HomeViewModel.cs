using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public ArticlePreviewCollection Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }

        public IRichTextContent IntroText { get; set; }
    }
}
