using Kentico.Kontent.Delivery;
using KenticoKontentBlog.KenticoModels;
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

        public ArticleController(ILogger<ArticleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("article/{articleStub}")]
        public IActionResult Index(string articleStub)
        {
            try
            {
                var client = DeliveryClientBuilder.WithProjectId(_configuration["KenticoKontent:DeliveryApi"]).Build();

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