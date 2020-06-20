using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Kontent.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModelBuilder : BaseViewModelBuilder<BlogArticle>, IArticleListViewModelBuilder
    {
        private string _categoryCodeName;

        public ArticleListViewModelBuilder(IDeliveryClientFactory deliveryClientFactory) : base(deliveryClientFactory)
        {
        }

        public IArticleListViewModelBuilder WithCategory(string categoryCodeName)
        {
            _categoryCodeName = categoryCodeName;

            return this;
        }

        public async Task<ArticleListViewModel> Build()
        {
            var articles = await this.ListAsync();
            var categoryName = "All Articles";

            if (!string.IsNullOrWhiteSpace(_categoryCodeName))
            {
                articles = articles?.Where(x => x.Categories != null && x.Categories.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                categoryName = articles.SelectMany(x => x.Categories).FirstOrDefault(x => x.Codename.Equals(_categoryCodeName))?.Name;
            }

            return articles == null ? null : new ArticleListViewModel
            {
                Articles = articles.Select(x => new ArticlePreview(x)).ToList(),
                CategoryName = categoryName
            };
        }
    }
}