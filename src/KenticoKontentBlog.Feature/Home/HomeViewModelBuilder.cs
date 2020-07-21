using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModelBuilder : IHomeViewModelBuilder
    {
        private readonly IDeliveryClient _client;

        public HomeViewModelBuilder(IDeliveryClientFactory deliveryClientFactory)
        {
            _client = deliveryClientFactory.Get();
        }

        public async Task<HomeViewModel> BuildAsync()
        {
            var homePage = await GetHomePage();

            return new HomeViewModel
            {
                Menu = await BuildMenuAsync(),
                Title = homePage?.HeroHeader,
                HeroImage = homePage?.HeroHeaderImage?.Select(x => x.Url).FirstOrDefault(),
                IntroText = homePage?.Introduction,
                Articles = homePage?.FeaturedContent?.Select(x => new ArticlePreview(x as ArticlePage)).ToList(),
                Seo = new SeoMetaData
                {
                    Title = string.IsNullOrWhiteSpace(homePage.SeoMetaDataMetaTitle) ? homePage.HeroHeader : homePage.SeoMetaDataMetaTitle,
                    Description = homePage.SeoMetaDataMetaDescription,
                    Image = homePage.SeoMetaDataMetaImages?.FirstOrDefault()?.Url ?? homePage.HeroHeaderImage?.FirstOrDefault()?.Url,
                    ContentType = Globals.Seo.ArticleContentType,
                    CanonicalUrl = "/",
                    TwitterAuthor = homePage.SeoMetaDataTwitterAccount?.Select(x => x.Name).FirstOrDefault() ?? Globals.Seo.TwitterSiteAuthor
                }
            };
        }

        private async Task<HomePage> GetHomePage()
        {
            var response = await _client.GetItemsAsync<HomePage>(new LimitParameter(1), new OrderParameter($"system.last_modified", SortOrder.Descending));

            return response.Items.FirstOrDefault();
        }

        private async Task<Menu> BuildMenuAsync()
        {
            try
            {
                var response = await _client.GetItemsAsync<ArticlePage>();

                return new Menu
                {
                    Categories = response.Items.SelectMany(x => x.Category).Distinct().ToDictionary(x => x.Codename, y => y.Name)
                };
            }
            catch (Exception)
            {
                return new Menu();
            }
        }
    }
}
