using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModel : IPageModel
    {
        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }

        public string Title { get; set; }

        public bool HasHeroImage => !string.IsNullOrWhiteSpace(HeroImage);

        public string HeroImage { get; set; }

        public IRichTextContent IntroText { get; set; }
    }
}
