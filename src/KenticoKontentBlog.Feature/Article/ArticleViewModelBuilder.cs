using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.Article
{
    public class ArticleViewModelBuilder : BaseViewModelBuilder<ArticleViewModel, BlogArticle>, IArticleViewModelBuilder
    {
        private string articleCodeName;

        public ArticleViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        public IArticleViewModelBuilder WithBlogArticle(string articleCodeName)
        {
            this.articleCodeName = articleCodeName;

            return this;
        }

        protected override async Task<ArticleViewModel> BuildModelAsync()
        {
            var article = await GetAsync(articleCodeName);

            return article == null ? null : new ArticleViewModel
            {
                Title = article?.Title,
                Content = article?.ArticleContent,
                HeroImage = article?.HeaderImage?.FirstOrDefault()?.Url,
                PublishedDate = article?.PublishedDate,
                Categories = article?.Categories?.ToDictionary(x => x.Codename, y => y.Name)
            };
        }
    }
}
