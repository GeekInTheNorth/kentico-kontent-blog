using System.Collections.Generic;

using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public class HtmlSiteMapViewModel : IPageModel
    {
        public HeroModel Hero { get; set; }

        public SeoMetaData Seo { get; set; }

        public Menu Menu { get; set; }

        public HtmlSiteMapItemCollectionViewModel SitePages { get; set; }

        public List<HtmlSiteMapItemCollectionViewModel> ArticleLists { get; set; }
    }
}
