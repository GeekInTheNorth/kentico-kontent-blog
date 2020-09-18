using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework.Builders;
using KenticoKontentBlog.Feature.Framework.Routing;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModelBuilder : IArticleViewModelBuilder
    {
        private string articleCodeName;

        private readonly IContentUrlHelper _urlHelper;

        private readonly IContentService _contentService;

        private readonly IArticlePreviewCollectionBuilder _previewCollectionBuilder;

        private readonly IDefaultBuilder _defaultBuilder;

        public ArticleViewModelBuilder(
            IContentService contentService,
            IContentUrlHelper urlHelper,
            IDefaultBuilder defaultBuilder,
            IArticlePreviewCollectionBuilder previewCollectionBuilder)
        {
            _contentService = contentService;
            _urlHelper = urlHelper;
            _defaultBuilder = defaultBuilder;
            _previewCollectionBuilder = previewCollectionBuilder;
        }

        public IArticleViewModelBuilder WithBlogArticle(string articleCodeName)
        {
            this.articleCodeName = articleCodeName;

            return this;
        }

        public async Task<ArticleViewModel> BuildAsync()
        {
            var article = await _contentService.GetContentAsync<ArticlePage>(articleCodeName);
            var relatedArticles = article?.RelatedArticles?.Where(x => x is ArticlePage).Select(x => x as ArticlePage);
            var relatedArticlesTitle = "Related Articles";
            var author = GetAuthorViewModel(article);

            if (article == null) return null;

            var model =  new ArticleViewModel
            {
                Content = article?.ArticleContent,
                Categories = article?.Category?.ToDictionary(x => x.Codename, y => y.Name),
                Author = author,
                RelatedArticles = _previewCollectionBuilder.Build(relatedArticlesTitle, relatedArticles)
            };

            _defaultBuilder.WithContent(article).WithModel(model).Build();

            model.Seo.TwitterAuthor = model.Author?.TwitterAccount ?? model.Seo.TwitterAuthor;

            return model;
        }

        private ArticleAuthorViewModel GetAuthorViewModel(ArticlePage article)
        {
            var author = article?.Author?.FirstOrDefault() as AuthorPage;

            if (author == null)
            {
                return null;
            }

            var authorImage = author?.ProfileImage?.FirstOrDefault();

            return new ArticleAuthorViewModel
            {
                Name = author?.Name,
                ProfileImage = authorImage,
                TwitterAccount = author?.TwitterAccount,
                FacebookUserName = author?.FacebookUserName,
                AuthorPage = _urlHelper.GetUrl(author)
            };
        }
    }
}
