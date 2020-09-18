using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(ArticlePage), "article_page"},
            {typeof(AuthorPage), "author_page"},
            {typeof(CodeSample), "code_sample"},
            {typeof(HomePage), "home_page"},
            {typeof(HtmlSiteMapPage), "html_site_map_page"},
            {typeof(ImageCarousel), "image_carousel"},
            {typeof(Quote), "quote"},
            {typeof(SiteSettings), "site_settings"},
            {typeof(YoutubeVideo), "youtube_video"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}