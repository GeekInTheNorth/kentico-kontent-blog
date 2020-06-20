using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using KenticoKontentBlog.Feature.Article;
using KenticoKontentBlog.Feature.ArticleList;
using KenticoKontentBlog.Feature.Home;
using KenticoKontentBlog.Feature.Kontent.Delivery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

            // View Model Builders
            services.AddTransient<IHomeViewModelBuilder, HomeViewModelBuilder>();
            services.AddTransient<IArticleViewModelBuilder, ArticleViewModelBuilder>();
            services.AddTransient<IArticleListViewModelBuilder, ArticleListViewModelBuilder>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}