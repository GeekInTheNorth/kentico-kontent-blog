using System.Threading.Tasks;
using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.Error
{
    public class ErrorViewModelBuilder : BaseViewModelBuilder<ErrorViewModel, ArticlePage>, IErrorViewModelBuilder
    {
        public ErrorViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        protected override Task<ErrorViewModel> BuildModelAsync()
        {
            return Task.FromResult(new ErrorViewModel());
        }
    }
}
