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

            var featuredArticles = homePage?.FeaturedContent?.Where(x => x is ArticlePage).Select(x => x as ArticlePage);
            var featuredArticlesTitle = "Featured Articles";

            return new HomeViewModel
            {
                Hero = new HeroModel
                {
                    Title = homePage?.HeroHeader,
                    Image = homePage?.HeroHeaderImage?.Select(x => x.Url).FirstOrDefault(),
                    HorizontalAlignment = homePage?.HeroImageHorizontalAlignment ?? ImageHorizontalAlignment.Centre,
                    VerticalAlignment = homePage?.HeroImageVerticalAlignment ?? ImageVerticalAlignment.Centre
                },
                Menu = await _contentService.GetCategoryMenuAsync(),
                IntroText = homePage?.Introduction,
                Articles = new ArticlePreviewCollection(featuredArticlesTitle, featuredArticles),
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
