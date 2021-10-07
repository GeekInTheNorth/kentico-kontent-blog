using System.Collections.Generic;
using System.Xml.Serialization;

namespace KenticoKontentBlog.Feature.RssFeed.Models
{
    public class RssChannelItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("pubDate")]
        public string PublishedDate { get; set; }

        [XmlElement("category")]
        public List<string> Category { get; set; }
    }
}
