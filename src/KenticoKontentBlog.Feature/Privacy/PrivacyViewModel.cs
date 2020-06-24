using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Privacy
{
    public class PrivacyViewModel : IPageModel
    {
        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
