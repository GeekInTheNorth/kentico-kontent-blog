using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.NotFound
{
    public class NotFoundViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public IRichTextContent Content { get; set; }

        public SeoMetaData Seo { get; set; }

        public Menu Menu { get; set; }
    }
}
