using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

using KenticoKontentBlog.Feature.Article;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Author;
using KenticoKontentBlog.Feature.Error;
using KenticoKontentBlog.Feature.Framework.Service;
using KenticoKontentBlog.Feature.Home;
using KenticoKontentBlog.Feature.HtmlSiteMap;
using KenticoKontentBlog.Feature.Kontent.Delivery;
using KenticoKontentBlog.Feature.Kontent.Models;
using KenticoKontentBlog.Feature.SiteMap;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KenticoKontentBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Enable Delivery Client
            services.AddHttpClient<IDeliveryHttpClient, DeliveryHttpClient>();
            services.AddSingleton<ITypeProvider, CustomTypeProvider>();
            services.AddSingleton<IContentLinkUrlResolver, CustomContentLinkUrlResolver>();
            services.AddDeliveryClient(Configuration);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

            // View Model Builders
            services.AddTransient<IHomeViewModelBuilder, HomeViewModelBuilder>();
            services.AddTransient<IArticleViewModelBuilder, ArticleViewModelBuilder>();
            services.AddTransient<IArticleListViewModelBuilder, ArticleListViewModelBuilder>();
            services.AddTransient<IErrorViewModelBuilder, ErrorViewModelBuilder>();
            services.AddTransient<ISiteMapBuilder, SiteMapBuilder>();
            services.AddTransient<IAuthorViewModelBuilder, AuthorViewModelBuilder>();
            services.AddTransient<IHtmlSiteMapViewModelBuilder, HtmlSiteMapViewModelBuilder>();

            services.AddTransient<IContentService, ContentService>();
            services.AddTransient<IArticlePreviewCollectionBuilder, ArticlePreviewCollectionBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
