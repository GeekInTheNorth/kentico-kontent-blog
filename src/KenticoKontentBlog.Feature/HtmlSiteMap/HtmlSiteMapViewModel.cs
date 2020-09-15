using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public class HtmlSiteMapViewModel
    {
        public HtmlSiteMapItemViewModel Home { get; set; }

        public HtmlSiteMapItemCollectionViewModel Authors { get; set; }

        public List<HtmlSiteMapItemCollectionViewModel> ArticleLists { get; set; }
    }
}
