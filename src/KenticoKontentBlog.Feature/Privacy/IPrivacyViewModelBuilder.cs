using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Privacy
{
    public interface IPrivacyViewModelBuilder
    {
        Task<PrivacyViewModel> BuildAsync();
    }
}
