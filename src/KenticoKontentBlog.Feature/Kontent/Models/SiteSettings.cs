﻿// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System.Collections.Generic;

using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class SiteSettings
    {
        public const string Codename = "site_settings";
        public const string NotFoundCopyCodename = "not_found_copy";
        public const string NotFoundImageCodename = "not_found_image";
        public const string NotFoundTitleCodename = "not_found_title";
        public const string ServerErrorCopyCodename = "server_error_copy";
        public const string ServerErrorImageCodename = "server_error_image";
        public const string ServerErrorTitleCodename = "server_error_title";
        public const string SiteNameCodename = "site_name";

        public IRichTextContent NotFoundCopy { get; set; }
        public IEnumerable<Asset> NotFoundImage { get; set; }
        public string NotFoundTitle { get; set; }
        public IRichTextContent ServerErrorCopy { get; set; }
        public IEnumerable<Asset> ServerErrorImage { get; set; }
        public string ServerErrorTitle { get; set; }
        public string SiteName { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}