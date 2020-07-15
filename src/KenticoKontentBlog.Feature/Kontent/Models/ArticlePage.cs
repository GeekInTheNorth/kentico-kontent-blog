﻿// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class ArticlePage
    {
        public const string Codename = "article_page";
        public const string ArticleCarouselCodename = "article_carousel";
        public const string ArticleContentCodename = "article_content";
        public const string CategoryCodename = "category";
        public const string HeroHeaderCodename = "hero__header";
        public const string HeroHeaderImageCodename = "hero__header_image";
        public const string PublishedDateCodename = "published_date";
        public const string RelatedArticlesCodename = "related_articles";
        public const string SeoMetaDataMetaDescriptionCodename = "seo_meta_data__meta_description";
        public const string SeoMetaDataMetaImagesCodename = "seo_meta_data__meta_images";
        public const string SeoMetaDataMetaTitleCodename = "seo_meta_data__meta_title";
        public const string SeoMetaDataTwitterAccountCodename = "seo_meta_data__twitter_account";

        public IEnumerable<Asset> ArticleCarousel { get; set; }
        public IRichTextContent ArticleContent { get; set; }
        public IEnumerable<TaxonomyTerm> Category { get; set; }
        public string HeroHeader { get; set; }
        public IEnumerable<Asset> HeroHeaderImage { get; set; }
        public DateTime? PublishedDate { get; set; }
        public IEnumerable<object> RelatedArticles { get; set; }
        public string SeoMetaDataMetaDescription { get; set; }
        public IEnumerable<Asset> SeoMetaDataMetaImages { get; set; }
        public string SeoMetaDataMetaTitle { get; set; }
        public IEnumerable<MultipleChoiceOption> SeoMetaDataTwitterAccount { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}