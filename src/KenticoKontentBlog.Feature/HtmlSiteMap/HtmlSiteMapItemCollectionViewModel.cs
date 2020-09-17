using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public class HtmlSiteMapItemCollectionViewModel
    {
        public HtmlSiteMapItemViewModel Parent { get; set; }

        public List<HtmlSiteMapItemViewModel> Children { get; set; }
    }
}
