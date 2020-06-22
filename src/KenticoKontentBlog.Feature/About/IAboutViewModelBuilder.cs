using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.About
{
    public interface IAboutViewModelBuilder
    {
        IAboutViewModelBuilder WithContentStub(string contentStub);

        Task<AboutViewModel> BuildAsync();
    }
}
