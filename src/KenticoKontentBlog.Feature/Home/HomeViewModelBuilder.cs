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
                Articles = articles.OrderByDescending(x => x.PublishedDate ?? x.System.LastModified)
                                   .Take(6)
                                   .Select(x => new ArticlePreview(x))
                                   .ToList()
            };
        }
    }
}
