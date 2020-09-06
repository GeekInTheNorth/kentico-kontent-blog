using System.Threading.Tasks;
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

            return new AuthorViewModel
            {
                Hero = new HeroModel { Title = $"Articles by {author.Name}" },
            };
        }
    }
}
