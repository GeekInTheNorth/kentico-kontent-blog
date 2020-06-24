namespace KenticoKontentBlog.Feature.Framework
{
    public interface IPageModel
    {
        SeoMetaData Seo { get; set; }

        Menu Menu { get; set; }
    }
}
