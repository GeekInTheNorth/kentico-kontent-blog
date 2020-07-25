using System.Linq;
using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.NotFound
{
    public class NotFoundViewModelBuilder : INotFoundViewModelBuilder
    {
        private readonly IContentService _contentService;

        public NotFoundViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<NotFoundViewModel> BuildAsync()
        {
            var menu = await _contentService.GetCategoriesAsync();
            var notFoundPages = await _contentService.GetLatestContentAsync<NotFoundPage>();
            var notFoundPage = notFoundPages.FirstOrDefault();

            return new NotFoundViewModel
            {
                Hero = new HeroModel
                {
                    Title = notFoundPage?.HeroHeader,
                    Image = notFoundPage?.HeroHeaderImage?.FirstOrDefault()?.Url
                },
                Content = notFoundPage?.NotFoundContent,
                Menu = menu
            };
        }
    }
}
