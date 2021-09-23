using System.Collections.Generic;
using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    public class RssChannelItem
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PublishedDate { get; set; }

        [XmlElement(ElementName = "lastBuildDate")]
        public string LastModifiedDate { get; set; }

        [XmlElement(ElementName = "category")]
        public List<string> Category { get; set; }
    }
}
