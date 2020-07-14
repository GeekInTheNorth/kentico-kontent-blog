using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Kontent.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KenticoKontentBlog.Feature.Kontent.Delivery
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            { typeof(ArticlePage), ArticlePage.Codename },
            { typeof(CodeSample), CodeSample.Codename },
            { typeof(HomePage), HomePage.Codename },
            { typeof(NotFoundPage), NotFoundPage.Codename }
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
