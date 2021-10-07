using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    [XmlType("rss")]
    public class Rss
    {
        public Rss()
        {
            Version = 2.0M;
        }

        [XmlElement("channel")]
        public RssChannel Channel { get; set; }

        [XmlAttribute("version")]
        public decimal Version { get; set; }
    }
}
