using System.Linq;

using KenticoKontentBlog.Feature.Framework.Routing;
using KenticoKontentBlog.Feature.Framework.Service;

namespace KenticoKontentBlog.Feature.Framework.Builders
{
    public class DefaultBuilder : IDefaultBuilder
    {
        private readonly IContentService _contentService;

        private readonly IContentUrlHelper _urlHelper;

        private IContentPage _content;

        private IPageModel _model;

        public DefaultBuilder(IContentUrlHelper urlHelper, IContentService contentService)
        {
            _urlHelper = urlHelper;
            _contentService = contentService;
        }

        public IDefaultBuilder WithContent(IContentPage content)
        {
            _content = content;

            return this;
        }

        public IDefaultBuilder WithModel(IPageModel model)
        {
            _model = model;

            return this;
        }

        public void Build()
        {
            if (_model == null) throw new ModelBuilderException($"{nameof(_model)} has not been instantiated.");
            if (_content == null) throw new ModelBuilderException($"{nameof(_content)} has not been instantiated.");

            _model.Menu = _contentService.GetCategoryMenuAsync().Result;
            _model.Hero = new HeroModel
                              {
                                  Title = _content.HeroHeader,
                                  Image = _content.HeroHeaderImage?.FirstOrDefault()?.Url,
                                  PublishedDate = _content.PublishedDate ?? _content.System.LastModified,
                                  HorizontalAlignment = _content.HeroImageHorizontalAlignment,
                                  VerticalAlignment = _content.HeroImageVerticalAlignment
                              };
            _model.Seo = new SeoMetaData
                             {
                                 Title = string.IsNullOrWhiteSpace(_content.SeoMetaDataMetaTitle) ? _content.HeroHeader : _content.SeoMetaDataMetaTitle,
                                 Description = _content.SeoMetaDataMetaDescription,
                                 Image = _content.SeoMetaDataMetaImages?.FirstOrDefault()?.Url ?? _content.HeroHeaderImage?.FirstOrDefault()?.Url,
                                 ContentType = Globals.Seo.ArticleContentType,
                                 TwitterAuthor = _content.SeoMetaDataTwitterAccount?.Select(x => x.Name).FirstOrDefault() ?? Globals.Seo.TwitterSiteAuthor,
                                 CanonicalUrl = _urlHelper.GetUrl(_content)
                             };
        }
    }
}
