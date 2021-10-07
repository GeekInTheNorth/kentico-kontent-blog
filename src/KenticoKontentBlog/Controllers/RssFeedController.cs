using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
            var xmlWriterSettings = new XmlWriterSettings { Indent = false, Encoding = Encoding.UTF8 };

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);

            serializer.Serialize(xmlWriter, rssFeedObject);
            var utf8EncodedXml = memoryStream.ToArray();

            return File(utf8EncodedXml, @"application/rss+xml");
        }
    }
}
