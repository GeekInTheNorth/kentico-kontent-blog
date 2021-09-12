namespace KenticoKontentBlog.Feature.Framework
{
    using System;
    using System.Collections.Generic;

    using Kentico.Kontent.Delivery.Abstractions;

    public interface IContentPage
    {
        string HeroHeader { get; }

        IEnumerable<IAsset> HeroHeaderImage { get; }

        ImageHorizontalAlignment HeroImageHorizontalAlignment { get; }

        ImageVerticalAlignment HeroImageVerticalAlignment { get; }

        HeaderTextColour HeroHeaderTextColour { get; }

        DateTime? PublishedDate { get; }

        string SeoMetaDataMetaDescription { get; }

        IEnumerable<IAsset> SeoMetaDataMetaImages { get; }

        string SeoMetaDataMetaTitle { get; }

        IEnumerable<IMultipleChoiceOption> SeoMetaDataTwitterAccount { get; }

        IContentItemSystemAttributes System { get; }
    }
}