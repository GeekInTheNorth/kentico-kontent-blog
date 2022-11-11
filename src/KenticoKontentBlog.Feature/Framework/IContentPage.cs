using System;
using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Framework
{
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