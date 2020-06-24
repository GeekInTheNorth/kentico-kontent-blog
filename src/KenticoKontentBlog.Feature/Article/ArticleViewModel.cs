using KenticoKontentBlog.Feature.Framework;
using System;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModel : IPageModel
    {
        public string Title { get; set; }

        public string HeroImage { get; set; }

        public bool HasHeroImage => !string.IsNullOrWhiteSpace(HeroImage);

        public string Content { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Dictionary<string, string> Categories { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
