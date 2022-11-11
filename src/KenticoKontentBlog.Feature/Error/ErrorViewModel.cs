using KenticoKontentBlog.Feature.Framework;

using Kontent.Ai.Delivery.Abstractions;

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
