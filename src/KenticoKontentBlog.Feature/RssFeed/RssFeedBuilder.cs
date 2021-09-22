using System.Threading.Tasks;

using KenticoKontentBlog.Feature.RssFeed.Models;

using Kentico.Kontent.Delivery.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.RssFeed
{
    public class RssFeedBuilder : IRssFeedBuilder
    {
        private readonly IDeliveryClient deliveryClient;

        private readonly IUrlHelper urlHelper;

        public RssFeedBuilder(
            IDeliveryClientFactory deliveryClientFactory,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            deliveryClient = deliveryClientFactory.Get();
            urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task<Rss> BuildAsync()
        {
            var articles = await GetArticles();

            return new Rss
            {
                BlogChannel = urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, Globals.Routing.DefaultProtocol),
                Channel = new RssChannel
                {
                    Title = "Stotty",
                    Description = "A blog by stotty",
                    Items = articles.ToList()
                }
            };
        }

        private async Task<IList<RssChannelItem>> GetArticles()
        {
            var nodes = new List<RssChannelItem>();
            var contentFeed = deliveryClient.GetItemsFeed<ArticlePage>();
            while (contentFeed.HasMoreResults)
            {
                var batch = await contentFeed.FetchNextBatchAsync();

                nodes.AddRange(batch.Items.Select(x => new RssChannelItem
                {
                    Title = x.SeoMetaDataMetaTitle,
                    Description = x.SeoMetaDataMetaDescription,
                    Link = urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = x.System.Codename }, Globals.Routing.DefaultProtocol),
                    PublishedDate = x.PublishedDate?.ToString("r") ?? x.System.LastModified.ToString("r"),
                    LastModifiedDate = x.System.LastModified.ToString("r"),
                    Category = x.Category?.Select(y => y.Name).ToList()
                }));
            }

            return nodes;
        }
    }
}
