using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Error
{
    public interface IErrorViewModelBuilder
    {
        IErrorViewModelBuilder WithStatusCode(int statusCode);

        Task<ErrorViewModel> BuildAsync();
    }
}
