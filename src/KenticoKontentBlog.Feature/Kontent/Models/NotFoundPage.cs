﻿// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class NotFoundPage
    {
        public const string Codename = "not_found_page";
        public const string HeroHeaderCodename = "hero__header";
        public const string HeroHeaderImageCodename = "hero__header_image";
        public const string NotFoundContentCodename = "not_found_content";

        public string HeroHeader { get; set; }
        public IEnumerable<Asset> HeroHeaderImage { get; set; }
        public string NotFoundContent { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}