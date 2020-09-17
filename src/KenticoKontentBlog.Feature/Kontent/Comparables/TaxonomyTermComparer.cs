using System;
using System.Collections.Generic;

using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentBlog.Feature.Kontent.Comparables
{
    public class TaxonomyTermComparer : IEqualityComparer<TaxonomyTerm>
    {
        public bool Equals(TaxonomyTerm x, TaxonomyTerm y)
        {
            return string.Equals(x.Codename, y.Codename, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(TaxonomyTerm obj)
        {
            return obj.Codename.GetHashCode();
        }
    }
}
