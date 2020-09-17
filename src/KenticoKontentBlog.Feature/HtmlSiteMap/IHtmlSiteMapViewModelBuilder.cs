using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.HtmlSiteMap
{
    public interface IHtmlSiteMapViewModelBuilder
    {
        Task<HtmlSiteMapViewModel> BuildAsync();
    }
}
