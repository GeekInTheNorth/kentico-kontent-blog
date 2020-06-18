using Kentico.Kontent.Delivery.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Kontent.Framework
{
    public class BaseViewModelBuilder<T>
    {
        private readonly IDeliveryClientFactory _deliveryClientFactory;

        public BaseViewModelBuilder(IDeliveryClientFactory deliveryClientFactory)
        {
            _deliveryClientFactory = deliveryClientFactory;
        }

        protected async Task<T> GetAsync(string codeName)
        {
            try
            {
                var client = _deliveryClientFactory.Get();

                var response = await client.GetItemAsync<T>(codeName);

                return response.Item;
            }
            catch (Exception)
            {
                return default;
            }
        }

        protected async Task<List<T>> ListAsync()
        {
            try
            {
                var client = _deliveryClientFactory.Get();

                var response = await client.GetItemsAsync<T>();

                return response.Items.ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }
    }
}
