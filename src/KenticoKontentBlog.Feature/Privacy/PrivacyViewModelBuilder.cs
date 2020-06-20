using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Privacy
{
    public class PrivacyViewModelBuilder : BaseViewModelBuilder<PrivacyViewModel, BlogArticle>, IPrivacyViewModelBuilder
    {
        public PrivacyViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        protected override Task<PrivacyViewModel> BuildModelAsync()
        {
            return Task.FromResult(new PrivacyViewModel());
        }
    }
}
