using System;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticlePreview
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string CodeName { get; set; }

        public string AuthorName { get; set; }

        public string AuthorUrl { get; set; }
        
        public DateTime PublishedDate { get; set; }
    }
}
