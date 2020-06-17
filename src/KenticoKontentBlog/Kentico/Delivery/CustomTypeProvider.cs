using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Kentico.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentBlog.Kentico.Delivery
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(BlogArticle), "blogarticle"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}
