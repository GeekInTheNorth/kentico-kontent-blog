using System.Threading.Tasks;
using KenticoKontentBlog.Feature.Framework.Service;

namespace KenticoKontentBlog.Feature.Error
{
    public class ErrorViewModelBuilder : IErrorViewModelBuilder
    {
        private readonly IContentService _contentService;

        public ErrorViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<ErrorViewModel> BuildAsync()
        {
            var model = await BuildModelAsync();

            if (model != null)
            {
                model.Menu = await _contentService.GetCategoriesAsync();
            }

            return model;
        }

        private Task<ErrorViewModel> BuildModelAsync()
        {
            return Task.FromResult(new ErrorViewModel());
        }
    }
}
