using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework.Builders;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Home
{
    public class HomeViewModelBuilder : IHomeViewModelBuilder
    {
        private readonly IContentService _contentService;

        private readonly IArticlePreviewCollectionBuilder _previewCollectionBuilder;

        private readonly IDefaultBuilder _defaultBuilder;

        public HomeViewModelBuilder(IContentService contentService, IArticlePreviewCollectionBuilder previewCollectionBuilder, IDefaultBuilder defaultBuilder)
        {
            _contentService = contentService;
            _previewCollectionBuilder = previewCollectionBuilder;
            _defaultBuilder = defaultBuilder;
        }

        public async Task<HomeViewModel> BuildAsync()
        {
            var homePage = await _contentService.GetLatestContentAsync<HomePage>();
            var featuredArticles = homePage?.FeaturedContent?.Where(x => x is ArticlePage).Select(x => x as ArticlePage);
            var featuredArticlesTitle = "Featured Articles";

            var model = new HomeViewModel
            {
                IntroText = homePage?.Introduction,
                Articles = _previewCollectionBuilder.Build(featuredArticlesTitle, featuredArticles)
            };

            _defaultBuilder.WithContent(homePage).WithModel(model).Build();

            return model;
        }
    }
}
