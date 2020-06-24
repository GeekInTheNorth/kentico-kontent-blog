using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModel : IPageModel
    {
        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }

        public SeoMetaData Seo { get; set; }
    }
}
