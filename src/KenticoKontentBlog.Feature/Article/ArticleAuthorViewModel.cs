using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleAuthorViewModel
    {
        public string Name { get; internal set; }
        
        public Asset ProfileImage { get; internal set; }
        
        public string TwitterAccount { get; internal set; }
        
        public string FacebookUserName { get; internal set; }
    }
}
