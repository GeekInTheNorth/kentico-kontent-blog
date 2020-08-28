using System;

namespace KenticoKontentBlog.Feature.Framework
{
    public class HeroModel
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public bool HasHeroImage { get; set; }

        public DateTime? PublishedDate { get; set; }

        public ImageHorizontalAlignment HorizontalAlignment { get; set; }

        public ImageVerticalAlignment VerticalAlignment { get; set; }
    }
}
