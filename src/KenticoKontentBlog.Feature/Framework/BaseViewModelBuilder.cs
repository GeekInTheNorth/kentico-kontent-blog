using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Kontent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Framework
{
    public abstract class BaseViewModelBuilder<TModel, TContent>
        where TModel : IPageModel
    {
        private readonly IDeliveryClientFactory _deliveryClientFactory;

        public BaseViewModelBuilder(IDeliveryClientFactory deliveryClientFactory)
        {
            _deliveryClientFactory = deliveryClientFactory;
        }

        protected abstract Task<TModel> BuildModelAsync();

        public async Task<TModel> BuildAsync()
        {
            var model = await BuildModelAsync();

            if(model != null)
            {
                model.Menu = await BuildMenuAsync();
            }

            return model;
        }

        protected async Task<TContent> GetAsync(string codeName)
        {
            try
            {
                var client = _deliveryClientFactory.Get();

                var response = await client.GetItemAsync<TContent>(codeName);

                return response.Item;
            }
            catch (Exception)
            {
                return default;
            }
        }

        protected async Task<List<TContent>> ListAsync()
        {
            try
            {
                var client = _deliveryClientFactory.Get();

                var response = await client.GetItemsAsync<TContent>();

                return response.Items.ToList();
            }
            catch (Exception)
            {
                return new List<TContent>();
            }
        }

        private async Task<Menu> BuildMenuAsync()
        {
            try
            {
                var client = _deliveryClientFactory.Get();
                var response = await client.GetItemsAsync<BlogArticle>();

                return new Menu
                {
                    Categories = response.Items.SelectMany(x => x.Categories).Distinct().ToDictionary(x => x.Codename, y => y.Name)
                };
            }
            catch (Exception)
            {
                return new Menu();
            }
        }
    }
}
