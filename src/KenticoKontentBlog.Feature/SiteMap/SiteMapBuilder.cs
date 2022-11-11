using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;

using Kontent.Ai.Delivery.Abstractions;
using Kontent.Ai.Urls.Delivery.QueryParameters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace KenticoKontentBlog.Feature.SiteMap
{
    public class SiteMapBuilder : ISiteMapBuilder
    {
        private readonly IDeliveryClient deliveryClient;

        private readonly IUrlHelper urlHelper;

        public SiteMapBuilder(
            IDeliveryClientFactory deliveryClientFactory, 
            IUrlHelperFactory urlHelperFactory, 
            IActionContextAccessor actionContextAccessor)
        {
            deliveryClient = deliveryClientFactory.Get();
            urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task<List<SitemapNode>> BuildAsync()
        {
            var nodes = new List<SitemapNode>();
            nodes.Add(new SitemapNode
            {
                Url = urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, null, Globals.Routing.DefaultProtocol),
                LastModified = DateTime.Today
            });

            var articleListings = await GetArticleListings();
            nodes.AddRange(articleListings);

            var articles = await GetArticles();
            nodes.AddRange(articles);

            var authors = await GetAuthors();
            nodes.AddRange(authors);

            return nodes;
        }

        private async Task<IEnumerable<SitemapNode>> GetArticleListings()
        {
            var siteSettingsResponse = await deliveryClient.GetItemsAsync<SiteSettings>(new LimitParameter(1), new OrderParameter($"system.last_modified", SortOrder.Descending));
            var siteSettings = siteSettingsResponse.Items.FirstOrDefault();

            return siteSettings.Category.Select(x => new SitemapNode
            {
                Url = urlHelper.Action(Globals.Routing.List, Globals.Routing.ArticleController, new { category = x.Codename }, Globals.Routing.DefaultProtocol),
                LastModified = DateTime.Today
            });
        }

        private async Task<IEnumerable<SitemapNode>> GetArticles()
        {
            var nodes = new List<SitemapNode>();
            var contentFeed = deliveryClient.GetItemsFeed<ArticlePage>();
            while (contentFeed.HasMoreResults)
            {
                var batch = await contentFeed.FetchNextBatchAsync();

                nodes.AddRange(batch.Items.Select(x => new SitemapNode
                {
                    Url = urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = x.System.Codename }, Globals.Routing.DefaultProtocol),
                    LastModified = x.System.LastModified
                }));
            }

            return nodes;
        }

        private async Task<IEnumerable<SitemapNode>> GetAuthors()
        {
            var nodes = new List<SitemapNode>();
            var contentFeed = deliveryClient.GetItemsFeed<AuthorPage>();
            while (contentFeed.HasMoreResults)
            {
                var batch = await contentFeed.FetchNextBatchAsync();

                nodes.AddRange(batch.Items.Select(x => new SitemapNode
                {
                    Url = urlHelper.Action(Globals.Routing.Index, Globals.Routing.AuthorController, new { authorCodeName = x.System.Codename }, Globals.Routing.DefaultProtocol),
                    LastModified = x.System.LastModified
                }));
            }

            return nodes;
        }
    }
}
