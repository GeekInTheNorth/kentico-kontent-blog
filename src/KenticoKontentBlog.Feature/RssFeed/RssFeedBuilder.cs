using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using KenticoKontentBlog.Feature.RssFeed.Models;

using Kontent.Ai.Delivery.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

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
                Channel = new RssChannel
                {
                    Title = "Stotty",
                    Description = "A blog by stotty",
                    LastModifiedDate = articles?.Max(x => x.System.LastModified).ToString("r"),
                    Link = urlHelper.Action(Globals.Routing.Index, Globals.Routing.HomeController, null, Globals.Routing.DefaultProtocol),
                    Items = articles.Select(ToRssItem).ToList()
                }
            };
        }

        private async Task<IList<ArticlePage>> GetArticles()
        {
            var articles = new List<ArticlePage>();
            var contentFeed = deliveryClient.GetItemsFeed<ArticlePage>();
            while (contentFeed.HasMoreResults)
            {
                var batch = await contentFeed.FetchNextBatchAsync();

                articles.AddRange(batch.Items);
            }

            return articles;
        }

        private RssChannelItem ToRssItem(ArticlePage articlePage)
        {
            return new RssChannelItem
            {
                Title = articlePage.SeoMetaDataMetaTitle,
                Description = articlePage.SeoMetaDataMetaDescription,
                Link = urlHelper.Action(Globals.Routing.Index, Globals.Routing.ArticleController, new { articleStub = articlePage.System.Codename }, Globals.Routing.DefaultProtocol),
                PublishedDate = articlePage.PublishedDate?.ToString("r") ?? articlePage.System.LastModified.ToString("r"),
                Category = articlePage.Category?.Select(y => y.Name).ToList()
            };
        }
    }
}
