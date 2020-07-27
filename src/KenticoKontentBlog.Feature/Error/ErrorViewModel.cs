using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Error
{
    public class ErrorViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public IRichTextContent Content { get; set; }
        
        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
