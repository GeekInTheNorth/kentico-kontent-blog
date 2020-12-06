using System;

namespace KenticoKontentBlog.Feature.Framework.Builders
{
    public class ModelBuilderException : Exception
    {
        public ModelBuilderException(string message) : base(message)
        {
        }
    }
}