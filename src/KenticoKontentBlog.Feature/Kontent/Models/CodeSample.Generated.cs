// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class CodeSample
    {
        public const string Codename = "code_sample";
        public const string CodeCodename = "code";
        public const string CodeLanguageCodename = "code_language";

        public IRichTextContent Code { get; set; }
        public IEnumerable<IMultipleChoiceOption> CodeLanguage { get; set; }
        public IContentItemSystemAttributes System { get; set; }
    }
}