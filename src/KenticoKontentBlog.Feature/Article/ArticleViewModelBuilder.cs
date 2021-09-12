using System.Collections.Generic;
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

        private const int MaxRelatedArticles = 3;

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
            var relatedArticles = GetRelatedArticles(article);
            if (relatedArticles.Count < MaxRelatedArticles)
            {
                var matchingArticles = await GetMatchingArticles(article, relatedArticles);
                relatedArticles.AddRange(matchingArticles);
            }

            relatedArticles = relatedArticles.Take(MaxRelatedArticles).ToList();
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

        private List<ArticlePage> GetRelatedArticles(ArticlePage currentArticlePage)
        {
            if (currentArticlePage?.RelatedArticles == null)
            {
                return new List<ArticlePage>();
            }

            return currentArticlePage.RelatedArticles.OfType<ArticlePage>().ToList();
        }

        private async Task<List<ArticlePage>> GetMatchingArticles(ArticlePage currentArticlePage, List<ArticlePage> relatedArticles)
        {
            var allArticles = await _contentService.GetListAsync<ArticlePage>();

            if (currentArticlePage.Category != null && currentArticlePage.Category.Any())
            {
                var categories = currentArticlePage.Category.Select(x => x.Name).ToList();
                allArticles = allArticles.Where(x => x.Category?.Any(y => categories.Contains(y.Name)) ?? false).ToList();
            }
            
            var codeNames = new List<string> { currentArticlePage.System.Codename };
            if (relatedArticles != null)
            {
                codeNames.AddRange(relatedArticles.Select(x => x.System.Codename));
            }

            return allArticles.Where(x => !codeNames.Contains(x.System.Codename)).ToList();
        }
    }
}
