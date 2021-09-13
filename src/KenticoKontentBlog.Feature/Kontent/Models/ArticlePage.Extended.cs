using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Extensions;

namespace KenticoKontentBlog.Feature.Kontent.Models
{
    public partial class ArticlePage : IContentPage
    {
        public ImageHorizontalAlignment HeroImageHorizontalAlignment => HeroHeaderImageHorizontalAlignment.GetHorizontalAlignment();

        public ImageVerticalAlignment HeroImageVerticalAlignment => HeroHeaderImageVerticalAlignment.GetVerticalAlignment();

        public HeaderTextColour HeroHeaderTextColour => HeroHeaderTextColours.GetTextColour();
    }
}
