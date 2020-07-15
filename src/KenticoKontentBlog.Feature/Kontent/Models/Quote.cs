namespace KenticoKontentBlog.Feature.Kontent.Models
{
    using Kentico.Kontent.Delivery.Abstractions;

    public partial class Quote
    {
        public const string Codename = "quote";
        public const string AuthorCodename = "author";
        public const string SourceNameCodename = "source_name";
        public const string SourceUrlCodename = "source_url";
        public const string QuotationCodename = "quotation";

        public string Author { get; set; }

        public string SourceName { get; set; }

        public string SourceUrl { get; set; }

        public IRichTextContent Quotation { get; set; }
    }
}
