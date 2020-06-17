using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Kentico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace KenticoKontentBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IDeliveryClientFactory _deliveryClientFactory;

        public ArticleController(ILogger<ArticleController> logger, IConfiguration configuration, IDeliveryClientFactory deliveryClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _deliveryClientFactory = deliveryClientFactory;
        }

        [Route("article/{articleStub}")]
        public IActionResult Index(string articleStub)
        {
            try
            {
                var client = _deliveryClientFactory.Get();

                var model = client.GetItemAsync<BlogArticle>(articleStub).Result.Item;

                return View(model);
            }
            catch (Exception exception)
            {
                _logger.LogError("Oops", exception);
                throw;
            }
        }
    }
}