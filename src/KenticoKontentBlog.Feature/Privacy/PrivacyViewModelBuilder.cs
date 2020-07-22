using KenticoKontentBlog.Feature.Framework.Service;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Privacy
{
    public class PrivacyViewModelBuilder : IPrivacyViewModelBuilder
    {
        private readonly IContentService _contentService;

        public PrivacyViewModelBuilder(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task<PrivacyViewModel> BuildAsync()
        {
            var model = await BuildModelAsync();

            if (model != null)
            {
                model.Menu = await _contentService.GetCategoriesAsync();
            }

            return model;
        }

        private Task<PrivacyViewModel> BuildModelAsync()
        {
            return Task.FromResult(new PrivacyViewModel());
        }
    }
}
