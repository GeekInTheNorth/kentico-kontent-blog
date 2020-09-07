using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.Author
{
    public class AuthorViewModelBuilder : IAuthorViewModelBuilder
    {
        private readonly IContentService _contentService;

        private string _authorCodeName;

        public AuthorViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public IAuthorViewModelBuilder WithAuthorCodeName(string authorCodeName)
        {
            _authorCodeName = authorCodeName;

            return this;
        }

        public async Task<AuthorViewModel> BuildAsync()
        {
            var author = await _contentService.GetContentAsync<AuthorPage>(_authorCodeName);

            if (author == null)
            {
                return null;
            }

            var articles = await _contentService.GetListAsync<ArticlePage>();
            var authorArticles = articles?.Where(x => IsArticleByAuthor(x, author.System.Codename)).Select(x => new ArticlePreview(x));

            return new AuthorViewModel
            {
                Hero = new HeroModel
                {
                    Title = author?.HeroHeader,
                    Image = author?.HeroHeaderImage?.FirstOrDefault()?.Url,
                    HorizontalAlignment = author?.HeroImageHorizontalAlignment ?? ImageHorizontalAlignment.Centre,
                    VerticalAlignment = author?.HeroImageVerticalAlignment ?? ImageVerticalAlignment.Centre
                },
                Biography = author?.Biography,
                TwitterAccount = author?.TwitterAccount,
                FacebookUserName = author?.FacebookUserName,
                Articles = authorArticles?.ToList(),
                Menu = await _contentService.GetCategoryMenuAsync()
            };
        }

        private bool IsArticleByAuthor(ArticlePage articlePage, string authorCodeName)
        {
            var articleAuthor = articlePage.Author?.FirstOrDefault() as AuthorPage;

            return articleAuthor?.System?.Codename?.Equals(authorCodeName) ?? false;
        }
    }
}
