using Kentico.Kontent.Delivery.Abstractions;
using System.Collections.Generic;

namespace KenticoKontentBlog.Kentico.Models
{
    public partial class BlogArticle
    {
        public const string Codename = "blogarticle";
        public const string ArticleContentCodename = "article_content";
        public const string HeaderImageCodename = "header_image";
        public const string ImageCarouselCodename = "image_carousel";
        public const string RelatedArticlesCodename = "related_articles";
        public const string SeoMetaDataSeoDescriptionCodename = "seo_meta_data__seo_description";
        public const string SeoMetaDataSeoImageCodename = "seo_meta_data__seo_image";
        public const string SeoMetaDataSeoTitleCodename = "seo_meta_data__seo_title";
        public const string SeoMetaDataTwitterAccountNameCodename = "seo_meta_data__twitter_account_name";
        public const string TitleCodename = "title";
        public const string UrlSlugCodename = "url_slug";

        public string ArticleContent { get; set; }

        public IEnumerable<Asset> HeaderImage { get; set; }

        public IEnumerable<Asset> ImageCarousel { get; set; }

        public IEnumerable<object> RelatedArticles { get; set; }

        public string SeoMetaDataSeoDescription { get; set; }

        public IEnumerable<Asset> SeoMetaDataSeoImage { get; set; }

        public string SeoMetaDataSeoTitle { get; set; }

        public IEnumerable<MultipleChoiceOption> SeoMetaDataTwitterAccountName { get; set; }

        public ContentItemSystemAttributes System { get; set; }

        public string Title { get; set; }

        public string UrlSlug { get; set; }
    }
}