using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Error
{
    public interface IErrorViewModelBuilder
    {
        Task<ErrorViewModel> BuildAsync();
    }
}
