using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using KenticoKontentBlog.Feature.RssFeed;
using KenticoKontentBlog.Feature.RssFeed.Models;

using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class RssFeedController : Controller
    {
        private readonly IRssFeedBuilder rssFeedBuilder;

        public RssFeedController(IRssFeedBuilder rssFeedBuilder)
        {
            this.rssFeedBuilder = rssFeedBuilder;
        }

        [Route("sitemap.xml")]
        [Produces("application/xml")]
        public async Task<IActionResult> Index()
        {
            var rssFeedObject = await rssFeedBuilder.BuildAsync();
            var serializer = new XmlSerializer(typeof(Rss));

            // TODO Serialize XML
            return null;
        }
    }
}
