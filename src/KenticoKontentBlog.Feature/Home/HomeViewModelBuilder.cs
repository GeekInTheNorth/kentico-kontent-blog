using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModelBuilder : BaseViewModelBuilder<HomeViewModel, BlogArticle>, IHomeViewModelBuilder
    {
        public HomeViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        protected override async Task<HomeViewModel> BuildModelAsync()
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
