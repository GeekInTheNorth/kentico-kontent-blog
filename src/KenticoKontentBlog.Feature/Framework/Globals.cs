﻿namespace KenticoKontentBlog.Feature.Framework
{
    public static class Globals
    {
        public class Routing
        {
            public const string List = "List";

            public const string Index = "Index";

            public const string ArticleController = "Article";

            public const string HomeController = "Home";

            public const string AuthorController = "Author";

            public const string DefaultProtocol = "https";
        }

        public class Seo
        {
            public const string ArticleContentType = "article";

            public const string ContentType = "website";

            public const string TwitterSiteAuthor = "@GeekInTheNorth";
        }

        public class CacheKeys
        {
            public const string SiteSettings = "site.settings";
        }
    }
}
