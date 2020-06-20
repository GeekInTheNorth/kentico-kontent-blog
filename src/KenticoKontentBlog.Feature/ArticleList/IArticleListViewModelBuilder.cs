using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public interface IArticleListViewModelBuilder
    {
        IArticleListViewModelBuilder WithCategory(string categoryCodeName);

        Task<ArticleListViewModel> BuildAsync();
    }
}
