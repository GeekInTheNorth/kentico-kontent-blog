using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Author
{
    public class AuthorViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public ArticlePreviewCollection Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }

        public IRichTextContent Biography { get; set; }

        public string TwitterAccount { get; set; }

        public string FacebookUserName { get; set; }
    }
}
