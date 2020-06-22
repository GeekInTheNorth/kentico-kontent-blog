using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class AboutUsPage
    {
        public const string Codename = "about_us";
        public const string ContentCodename = "content";
        public const string HeaderCodename = "header";
        public const string HeaderImageCodename = "header_image";

        public string Content { get; set; }
        public string Header { get; set; }
        public IEnumerable<Asset> HeaderImage { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
