namespace KenticoKontentBlog.Feature.Framework.Builders
{
    public interface IDefaultBuilder
    {
        IDefaultBuilder WithContent(IContentPage content);

        IDefaultBuilder WithModel(IPageModel model);

        void Build();
    }
}