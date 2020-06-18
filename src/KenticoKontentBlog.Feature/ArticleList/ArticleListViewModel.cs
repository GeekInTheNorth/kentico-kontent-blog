using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModel
    {
        public string CategoryName { get; set; }

        public List<ArticlePreview> Articles { get; set; }
    }
}
