using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModelBuilder : IHomeViewModelBuilder
    {
        private readonly IContentService _contentService;

        public HomeViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<HomeViewModel> BuildAsync()
        {
            var results = await _contentService.GetLatestContentAsync<HomePage>();
            var homePage = results?.FirstOrDefault() ?? new HomePage();

            return new HomeViewModel
            {
                Hero = new HeroModel
                {
                    Title = homePage?.HeroHeader,
                    Image = homePage?.HeroHeaderImage?.Select(x => x.Url).FirstOrDefault(),
                },
                Menu = await _contentService.GetCategoryMenuAsync(),
                IntroText = homePage?.Introduction,
                Articles = homePage?.FeaturedContent?.Where(x => x is ArticlePage).Select(x => new ArticlePreview(x as ArticlePage)).OrderByDescending(x => x.PublishedDate).ToList(),
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
    }
}
