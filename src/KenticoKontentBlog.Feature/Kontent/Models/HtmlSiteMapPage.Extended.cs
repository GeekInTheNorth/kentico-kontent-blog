using System;
using System.Linq;

using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class HtmlSiteMapPage : IContentPage
    {
        public ImageHorizontalAlignment HeroImageHorizontalAlignment
        {
            get
            {
                if (HeroHeaderImageHorizontalAlignment.Any(x => x.Codename.Equals("left")))
                {
                    return ImageHorizontalAlignment.Left;
                }

                if (HeroHeaderImageHorizontalAlignment.Any(x => x.Codename.Equals("right")))
                {
                    return ImageHorizontalAlignment.Right;
                }

                return ImageHorizontalAlignment.Centre;
            }
        }

        public ImageVerticalAlignment HeroImageVerticalAlignment
        {
            get
            {
                if (HeroHeaderImageVerticalAlignment.Any(x => x.Codename.Equals("top")))
                {
                    return ImageVerticalAlignment.Top;
                }

                if (HeroHeaderImageVerticalAlignment.Any(x => x.Codename.Equals("bottom")))
                {
                    return ImageVerticalAlignment.Bottom;
                }

                return ImageVerticalAlignment.Centre;
            }
        }

        public DateTime? PublishedDate => null;
    }
}
