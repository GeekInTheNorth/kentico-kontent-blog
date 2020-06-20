﻿using KenticoKontentBlog.Feature.Article;
using KenticoKontentBlog.Feature.ArticleList;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace KenticoKontentBlog.Controllers
{
    public class ArticleController : Controller
    {
        public readonly IArticleViewModelBuilder _viewModelBuilder;

        public readonly IArticleListViewModelBuilder _listViewModelBuilder;

        public ArticleController(IArticleViewModelBuilder viewModelBuilder, IArticleListViewModelBuilder listViewModelBuilder)
        {
            _viewModelBuilder = viewModelBuilder;
            _listViewModelBuilder = listViewModelBuilder;
        }

        [Route("article/{articleStub}")]
        public async Task<IActionResult> IndexAsync(string articleStub)
        {
            if (!string.IsNullOrWhiteSpace(articleStub) && articleStub.Equals("list"))
            {
                return await ListAsync(null);
            }

            var article = await _viewModelBuilder.WithBlogArticle(articleStub).BuildAsync();

            if (article == null)
            {
                return NotFound();
            }

            return View("~/Views/Article/Index.cshtml", article);
        }

        [Route("article/list/{category}")]
        public async Task<IActionResult> ListAsync(string category)
        {
            var articleList = await _listViewModelBuilder.WithCategory(category).Build();

            if (articleList?.Articles == null || !articleList.Articles.Any())
            {
                return NotFound();
            }

            return View("~/Views/Article/List.cshtml", articleList);
        }
    }
}