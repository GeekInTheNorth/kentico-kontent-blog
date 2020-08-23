using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Kontent.Models;

using Microsoft.Extensions.Caching.Memory;

namespace KenticoKontentBlog.Feature.Framework.Service
{
    public class ContentService : IContentService
    {
        private readonly IDeliveryClient deliveryClient;

        private readonly IMemoryCache memoryCache;

        public ContentService(
            IDeliveryClientFactory deliveryClientFactory, 
            IMemoryCache memoryCache)
        {
            deliveryClient = deliveryClientFactory.Get();
            this.memoryCache = memoryCache;
        }

        public async Task<Menu> GetCategoryMenuAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue<SiteSettings>(Globals.CacheKeys.SiteSettings, out var siteSettings))
                {
                    var response = await GetLatestContentAsync<SiteSettings>();
                    siteSettings = response.First();

                    memoryCache.Set(Globals.CacheKeys.SiteSettings, siteSettings);
                }
                
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
                var cacheKey = $"content.{codeName}";
                if (!memoryCache.TryGetValue<TContent>(cacheKey, out var contentItem))
                {
                    var response = await deliveryClient.GetItemAsync<TContent>(codeName, new DepthParameter(1));

                    contentItem = response.Item;

                    memoryCache.Set(cacheKey, contentItem);
                }

                return contentItem;
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
                var cacheKey = $"content.list.{typeof(TContent).Name}";
                if (!memoryCache.TryGetValue<List<TContent>>(cacheKey, out var itemList))
                {
                    var response = await deliveryClient.GetItemsAsync<TContent>();

                    itemList = response.Items.ToList();

                    memoryCache.Set(cacheKey, itemList);
                }

                return itemList;
            }
            catch (Exception)
            {
                return new List<TContent>();
            }
        }
    }
}
