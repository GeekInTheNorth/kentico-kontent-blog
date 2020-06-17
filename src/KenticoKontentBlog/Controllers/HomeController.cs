using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KenticoKontentBlog.Models;
using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Kentico.Models;
using Kentico.Kontent.Delivery;

namespace KenticoKontentBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDeliveryClientFactory _deliveryClientFactory;

        public HomeController(ILogger<HomeController> logger, IDeliveryClientFactory deliveryClientFactory)
        {
            _logger = logger;
            _deliveryClientFactory = deliveryClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _deliveryClientFactory.Get();
            var response = await client.GetItemsAsync<BlogArticle>();

            var model = new HomeViewModel { Articles = response.Items.ToList() };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
