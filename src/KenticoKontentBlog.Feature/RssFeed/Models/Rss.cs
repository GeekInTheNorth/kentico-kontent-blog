using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    public class Rss
    {
        [XmlElement(ElementName = "channel")]
        public RssChannel Channel { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public decimal Version => 2.0M;

        [XmlAttribute(AttributeName = "xmlns:blogChannel")]
        public string BlogChannel { get; set; } 
    }
}
