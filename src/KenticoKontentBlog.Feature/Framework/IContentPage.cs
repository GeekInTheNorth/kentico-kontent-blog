namespace KenticoKontentBlog.Feature.Framework
{
    using System;
    using System.Collections.Generic;

    using Kentico.Kontent.Delivery.Abstractions;

    public interface IContentPage
    {
        string HeroHeader { get; }

        IEnumerable<Asset> HeroHeaderImage { get; }

        ImageHorizontalAlignment HeroImageHorizontalAlignment { get; }

        ImageVerticalAlignment HeroImageVerticalAlignment { get; }

        DateTime? PublishedDate { get; }

        string SeoMetaDataMetaDescription { get; }

        IEnumerable<Asset> SeoMetaDataMetaImages { get; }

        string SeoMetaDataMetaTitle { get; }

        IEnumerable<MultipleChoiceOption> SeoMetaDataTwitterAccount { get; }

        ContentItemSystemAttributes System { get; }
    }
}