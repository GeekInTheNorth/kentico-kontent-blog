using System;
using System.Collections.Generic;

using Kontent.Ai.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Comparables
{
    public class TaxonomyTermComparer : IEqualityComparer<ITaxonomyTerm>
    {
        public bool Equals(ITaxonomyTerm x, ITaxonomyTerm y)
        {
            return string.Equals(x.Codename, y.Codename, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(ITaxonomyTerm obj)
        {
            return obj.Codename.GetHashCode();
        }
    }
}
