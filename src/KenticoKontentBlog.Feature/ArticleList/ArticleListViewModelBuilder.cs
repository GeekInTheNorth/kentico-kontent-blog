using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Framework;
using KenticoKontentBlog.Feature.Kontent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Feature.ArticleList
{
    public class ArticleListViewModelBuilder : BaseViewModelBuilder<ArticleListViewModel, BlogArticle>, IArticleListViewModelBuilder
    {
        private string _categoryCodeName;

        private readonly IUrlHelper _urlHelper;

        public ArticleListViewModelBuilder(
            IDeliveryClientFactory deliveryClientFactory,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor) : 
            base(deliveryClientFactory)
        {
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public IArticleListViewModelBuilder WithCategory(string categoryCodeName)
        {
            _categoryCodeName = categoryCodeName;

            return this;
        }

        protected override async Task<ArticleListViewModel> BuildModelAsync()
        {
            var articles = await this.ListAsync();
            var categoryName = "All Articles";

            if (!string.IsNullOrWhiteSpace(_categoryCodeName))
            {
                articles = articles?.Where(x => x.Categories != null && x.Categories.Any(y => y.Codename.Equals(_categoryCodeName))).ToList();
                categoryName = articles.SelectMany(x => x.Categories).FirstOrDefault(x => x.Codename.Equals(_categoryCodeName))?.Name;
                categoryName = $"{categoryName} Articles";
            }

            return articles == null ? null : new ArticleListViewModel
            {
                Articles = articles.Select(x => new ArticlePreview(x)).ToList(),
                CategoryName = categoryName,
                Seo = new SeoMetaData
                {
                    Title = categoryName,
                    ContentType = "website",
                    CanonicalUrl = _urlHelper.Action("List", "Article", new { category = _categoryCodeName }, "https")
                }
            };
        }
    }
}