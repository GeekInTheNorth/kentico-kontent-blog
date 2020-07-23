using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.NotFound
{
    public class NotFoundViewModel : IPageModel
    {
        public string Title { get; set; }

        public string HeroImage { get; set; }

        public bool HasHeroImage => !string.IsNullOrWhiteSpace(HeroImage);

        public IRichTextContent Content { get; set; }

        public SeoMetaData Seo { get; set; }

        public Menu Menu { get; set; }
    }
}
