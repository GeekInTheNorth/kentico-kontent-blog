using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Kontent.Models;

namespace KenticoKontentBlog.Feature.Framework.Service
{
    public class ContentService : IContentService
    {
        protected readonly IDeliveryClient deliveryClient;

        public ContentService(IDeliveryClientFactory deliveryClientFactory)
        {
            deliveryClient = deliveryClientFactory.Get();
        }

        public async Task<Menu> GetCategoryMenuAsync()
        {
            try
            {
                var response = await GetLatestContentAsync<SiteSettings>();
                var siteSettings = response.FirstOrDefault();
                
                return new Menu
                {
                    Categories = siteSettings.Category.ToDictionary(x => x.Codename, y => y.Name)
                };
            }
            catch (Exception)
            {
                return new Menu();
            }
        }

        public async Task<TContent> GetContentAsync<TContent>(string codeName)
        {
            try
            {
                var response = await deliveryClient.GetItemAsync<TContent>(codeName, new DepthParameter(1));

                return response.Item;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<List<TContent>> GetLatestContentAsync<TContent>(int items = 1)
        {
            try
            {
                var response = await deliveryClient.GetItemsAsync<TContent>(new LimitParameter(items), new OrderParameter($"system.last_modified", SortOrder.Descending));

                return response.Items.ToList();
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<List<TContent>> GetListAsync<TContent>()
        {
            try
            {
                var response = await deliveryClient.GetItemsAsync<TContent>();

                return response.Items.ToList();
            }
            catch (Exception)
            {
                return new List<TContent>();
            }
        }
    }
}
