using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Kontent.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModelBuilder : BaseViewModelBuilder<BlogArticle>, IHomeViewModelBuilder
    {
        public HomeViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        public async Task<HomeViewModel> BuildAsync()
        {
            var articles = await this.ListAsync();

            return new HomeViewModel
            {
                Articles = articles.Select(x => Convert(x)).ToList()
            };
        }

        private ArticlePreview Convert(BlogArticle blogArticle)
        {
            return new ArticlePreview
            {
                Title = blogArticle.Title,
                Description = blogArticle.SeoMetaDataSeoDescription,
                Image = blogArticle.HeaderImage?.FirstOrDefault()?.Url,
                CodeName = blogArticle.System.Codename
            };
        }
    }
}
