using System;
using System.Linq;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class YoutubeVideo
    {
        public bool AutoPlay => GetValue("autoplay");

        public bool AllowFullscreen => GetValue("allow_fullscreen");

        private bool GetValue(string codeName)
        {
            return Options?.Any(x => x.Codename.Equals(codeName, StringComparison.CurrentCultureIgnoreCase)) ?? false;
        }
    }
}
