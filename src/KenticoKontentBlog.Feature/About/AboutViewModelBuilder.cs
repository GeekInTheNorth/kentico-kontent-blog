using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.About
{
    public class AboutViewModelBuilder : BaseViewModelBuilder<AboutViewModel, AboutUsPage>, IAboutViewModelBuilder
    {
        private string _contentStub;

        public AboutViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        public IAboutViewModelBuilder WithContentStub(string contentStub)
        {
            _contentStub = contentStub;

            return this;
        }

        protected override async Task<AboutViewModel> BuildModelAsync()
        {
            var content = await GetAsync(_contentStub);

            return content == null ? null : new AboutViewModel
            {
                Title = content?.Header,
                Content = content?.Content,
                HeroImage = content?.HeaderImage?.FirstOrDefault()?.Url,
            };
        }
    }
}
