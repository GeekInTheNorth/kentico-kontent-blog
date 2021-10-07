using System.Collections.Generic;
using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    public class RssChannel
    {
        public RssChannel()
        {
            Language = "en-GB";
        }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "language")]
        public string Language { get; set; }

        [XmlElement(ElementName = "lastBuildDate")]
        public string LastModifiedDate { get; set; }

        [XmlElement(ElementName = "item")]
        public List<RssChannelItem> Items { get; set; }
    }
}
