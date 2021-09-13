using KenticoKontentBlog.Feature.Kontent.Extensions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class YoutubeVideo
    {
        public bool AutoPlay => Options.GetBoolean("autoplay");

        public bool AllowFullscreen => Options.GetBoolean("allow_fullscreen");
    }
}
