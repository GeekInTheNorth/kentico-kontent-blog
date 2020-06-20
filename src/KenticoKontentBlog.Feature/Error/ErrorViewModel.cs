using System.Collections.Generic;
using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Error
{
    public class ErrorViewModel : IPageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Menu Menu { get; set; }
    }
}
