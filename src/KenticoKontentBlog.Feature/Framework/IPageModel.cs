namespace KenticoKontentBlog.Feature.Framework
{
    public interface IPageModel
    {
        HeroModel Hero { get; set; }

        SeoMetaData Seo { get; set; }

        Menu Menu { get; set; }
    }
}
