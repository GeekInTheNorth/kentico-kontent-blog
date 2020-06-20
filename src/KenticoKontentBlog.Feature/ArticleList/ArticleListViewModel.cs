using KenticoKontentBlog.Feature.Framework;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModel : IPageModel
    {
        public string CategoryName { get; set; }

        public List<ArticlePreview> Articles { get; set; }

        public Menu Menu { get; set; }
    }
}
