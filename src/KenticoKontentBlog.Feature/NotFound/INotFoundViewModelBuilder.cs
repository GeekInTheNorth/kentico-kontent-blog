using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.NotFound
{
    public interface INotFoundViewModelBuilder
    {
        Task<NotFoundViewModel> BuildAsync();
    }
}
