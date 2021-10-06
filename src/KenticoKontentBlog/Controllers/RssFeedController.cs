using System.IO;
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

        [Route("feed")]
        [Produces("application/xml")]
        public async Task<IActionResult> Index()
        {
            var rssFeedObject = await rssFeedBuilder.BuildAsync();
            var serializer = new XmlSerializer(typeof(Rss));

            using var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, rssFeedObject);

            return new ContentResult
            {
                Content = stringWriter.ToString(),
                ContentType = @"application/rss+xml",
                StatusCode = 200
            };
        }
    }
}
