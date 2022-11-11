using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleAuthorViewModel
    {
        public string Name { get; internal set; }
        
        public IAsset ProfileImage { get; internal set; }
        
        public string TwitterAccount { get; internal set; }
        
        public string FacebookUserName { get; internal set; }

        public string AuthorPage { get; internal set; }

        public bool IsPopulated => !string.IsNullOrWhiteSpace(Name) && ProfileImage != null;
    }
}