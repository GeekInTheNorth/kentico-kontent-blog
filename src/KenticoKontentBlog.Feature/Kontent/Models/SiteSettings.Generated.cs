// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class SiteSettings
    {
        public const string Codename = "site_settings";
        public const string CategoryCodename = "category";
        public const string NotFoundCopyCodename = "not_found_copy";
        public const string NotFoundImageCodename = "not_found_image";
        public const string NotFoundTitleCodename = "not_found_title";
        public const string ServerErrorCopyCodename = "server_error_copy";
        public const string ServerErrorImageCodename = "server_error_image";
        public const string ServerErrorTitleCodename = "server_error_title";
        public const string SiteNameCodename = "site_name";

        public IEnumerable<ITaxonomyTerm> Category { get; set; }
        public IRichTextContent NotFoundCopy { get; set; }
        public IEnumerable<IAsset> NotFoundImage { get; set; }
        public string NotFoundTitle { get; set; }
        public IRichTextContent ServerErrorCopy { get; set; }
        public IEnumerable<IAsset> ServerErrorImage { get; set; }
        public string ServerErrorTitle { get; set; }
        public string SiteName { get; set; }
        public IContentItemSystemAttributes System { get; set; }
    }
}