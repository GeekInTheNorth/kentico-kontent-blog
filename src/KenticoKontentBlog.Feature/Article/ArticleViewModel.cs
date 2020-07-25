using System.Collections.Generic;

using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public IRichTextContent Content { get; set; }

        public Dictionary<string, string> Categories { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
