namespace KenticoKontentBlog.Feature.Framework.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContentService
    {
        Task<Menu> GetCategoryMenuAsync();

        Task<TContent> GetContentAsync<TContent>(string codeName);

        Task<TContent> GetLatestContentAsync<TContent>();

        Task<List<TContent>> GetListAsync<TContent>();
    }
}