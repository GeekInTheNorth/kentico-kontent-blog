using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public interface IHomeViewModelBuilder
    {
        Task<HomeViewModel> BuildAsync();
    }
}
