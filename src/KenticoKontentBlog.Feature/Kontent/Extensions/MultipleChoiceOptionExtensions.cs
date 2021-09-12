using System;
using System.Collections.Generic;
using System.Linq;

using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Framework;

namespace KenticoKontentBlog.Feature.Kontent.Extensions
{
    public static class MultipleChoiceOptionExtensions
    {
        public static ImageHorizontalAlignment GetHorizontalAlignment(this IEnumerable<IMultipleChoiceOption> options)
        {
            if (options.Any(x => x.Codename.Equals("left")))
            {
                return ImageHorizontalAlignment.Left;
            }

            if (options.Any(x => x.Codename.Equals("right")))
            {
                return ImageHorizontalAlignment.Right;
            }

            return ImageHorizontalAlignment.Centre;
        }

        public static ImageVerticalAlignment GetVerticalAlignment(this IEnumerable<IMultipleChoiceOption> options)
        {
            if (options.Any(x => x.Codename.Equals("top")))
            {
                return ImageVerticalAlignment.Top;
            }

            if (options.Any(x => x.Codename.Equals("bottom")))
            {
                return ImageVerticalAlignment.Bottom;
            }

            return ImageVerticalAlignment.Centre;
        }

        public static HeaderTextColour GetTextColour(this IEnumerable<IMultipleChoiceOption> options)
        {
            if (options.Any(x => x.Codename.Equals("dark")))
            {
                return HeaderTextColour.Dark;
            }

            return HeaderTextColour.Light;
        }

        public static bool GetBoolean(this IEnumerable<IMultipleChoiceOption> options, string codeName)
        {
            return options?.Any(x => x.Codename.Equals(codeName, StringComparison.CurrentCultureIgnoreCase)) ?? false;
        }
    }
}
