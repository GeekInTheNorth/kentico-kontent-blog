using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }

        public IRichTextContent IntroText { get; set; }
    }
}
