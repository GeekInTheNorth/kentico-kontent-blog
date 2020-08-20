using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;

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

            var contentFeed = deliveryClient.GetItemsFeed<ArticlePage>();
            while (contentFeed.HasMoreResults)
            {
                var batch = await contentFeed.FetchNextBatchAsync();

                foreach(var contentItem in batch.Items)
                {
                    nodes.Add(new SitemapNode
                    {
                        Url = urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = contentItem.System.Codename }, Globals.Routing.DefaultProtocol),
                        LastModified = contentItem.System.LastModified
                    });
                }
            }

            return nodes;
        }
    }
}
