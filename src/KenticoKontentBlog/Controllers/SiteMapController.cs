using System.Text;
using System.Threading.Tasks;

using KenticoKontentBlog.Feature.SiteMap;

using Microsoft.AspNetCore.Mvc;

namespace KenticoKontentBlog.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly ISiteMapBuilder builder;

        public SiteMapController(ISiteMapBuilder builder)
        {
            this.builder = builder;
        }

        [Route("sitemap.xml")]
        [Produces("application/xml")]
        public async Task<IActionResult> XmlSiteMap()
        {
            var nodes = await builder.BuildAsync();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            foreach(var node in nodes)
            {
                stringBuilder.Append($"<url><loc>{node.Url}</loc><lastmod>{node.LastModified:s}</lastmod></url>");
            }

            stringBuilder.Append("</urlset>");

            return new ContentResult
            {
                Content = stringBuilder.ToString(),
                ContentType = "application/xml",
                StatusCode = 200
            };
        }
    }
}
