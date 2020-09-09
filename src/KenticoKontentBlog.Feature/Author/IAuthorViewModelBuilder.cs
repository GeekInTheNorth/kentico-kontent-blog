using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Author
{
    public interface IAuthorViewModelBuilder
    {
        IAuthorViewModelBuilder WithAuthorCodeName(string authorCodeName);

        Task<AuthorViewModel> BuildAsync();
    }
}
