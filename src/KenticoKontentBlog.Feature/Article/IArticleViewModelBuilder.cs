using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Article
{
    public interface IArticleViewModelBuilder
    {
        IArticleViewModelBuilder WithBlogArticle(string articleCodeName);

        Task<ArticleViewModel> BuildAsync();
    }
}
