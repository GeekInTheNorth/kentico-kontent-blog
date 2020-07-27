using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.Error
{
    public class ErrorViewModelBuilder : IErrorViewModelBuilder
    {
        private readonly IContentService _contentService;

        private int _statusCode;

        public ErrorViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public IErrorViewModelBuilder WithStatusCode(int statusCode)
        {
            _statusCode = statusCode;

            return this;
        }

        public async Task<ErrorViewModel> BuildAsync()
        {
            var response = await _contentService.GetLatestContentAsync<SiteSettings>();
            var siteSettings = response.FirstOrDefault();
            var menu = await _contentService.GetCategoriesAsync();

            var model = new ErrorViewModel
            {
                Hero = new HeroModel
                {
                    Title = _statusCode.Equals(404) ? siteSettings?.NotFoundTitle : siteSettings?.ServerErrorTitle,
                    Image = _statusCode.Equals(404) ? siteSettings?.NotFoundImage?.FirstOrDefault()?.Url : siteSettings?.ServerErrorImage?.FirstOrDefault()?.Url,
                },
                Content = _statusCode.Equals(404) ? siteSettings?.NotFoundCopy : siteSettings?.ServerErrorCopy,
                Menu = menu
            };

            return model;
        }
    }
}
