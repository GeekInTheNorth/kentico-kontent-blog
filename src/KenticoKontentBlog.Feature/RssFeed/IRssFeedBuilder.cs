using System.Threading.Tasks;

using KenticoKontentBlog.Feature.RssFeed.Models;

namespace KenticoKontentBlog.Feature.RssFeed
{
    public interface IRssFeedBuilder
    {
        Task<Rss> BuildAsync();
    }
}
