using System;
using System.Collections.Generic;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;

using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public IRichTextContent Content { get; set; }

        public DateTime? PublishedDate => Hero?.PublishedDate;

        public ArticleAuthorViewModel Author { get; set; }

        public Dictionary<string, string> Categories { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }

        public ArticlePreviewCollection RelatedArticles { get; set; }
    }
}
