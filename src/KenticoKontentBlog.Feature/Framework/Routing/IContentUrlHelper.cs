namespace KenticoKontentBlog.Feature.Framework.Routing
{
    public interface IContentUrlHelper
    {
        string GetUrl(IContentPage contentPage);

        string GetListingUrl(string categoryCodeName);
    }
}
