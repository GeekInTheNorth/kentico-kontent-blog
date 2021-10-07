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

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("lastBuildDate")]
        public string LastModifiedDate { get; set; }

        [XmlElement("item")]
        public List<RssChannelItem> Items { get; set; }
    }
}
