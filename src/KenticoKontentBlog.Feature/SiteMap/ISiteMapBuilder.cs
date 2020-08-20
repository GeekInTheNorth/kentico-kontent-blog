using System.Collections.Generic;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.SiteMap
{
    public interface ISiteMapBuilder
    {
        Task<List<SitemapNode>> BuildAsync();
    }
}
