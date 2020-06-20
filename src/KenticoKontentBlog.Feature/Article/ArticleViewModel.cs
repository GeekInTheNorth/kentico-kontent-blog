using System;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModel
    {
        public string Title { get; set; }

        public string HeroImage { get; set; }

        public bool HasHeroImage => !string.IsNullOrWhiteSpace(HeroImage);

        public string Content { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Dictionary<string, string> Categories { get; set; }
    }
}
