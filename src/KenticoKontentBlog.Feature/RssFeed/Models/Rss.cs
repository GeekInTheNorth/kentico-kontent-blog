using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    public class Rss
    {
        [XmlElement("channel")]
        public RssChannel Channel { get; set; }

        [XmlAttribute("version")]
        public decimal Version => 2.0M;
    }
}
