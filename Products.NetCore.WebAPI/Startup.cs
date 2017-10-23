using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.NetCore.Repository;
using Products.NetCore.Repository.Helpers;
using Products.NetCore.Repository.Helpers.Interfaces;
using Products.NetCore.Repository.Interfaces;
using Products.NetCore.Service;
using Products.NetCore.Service.Interfaces;
using Products.NetCore.WebAPI.Helpers.Filters;
using Products.NetCore.WebAPI.Controllers.v1_0;

namespace Products.NetCore.WebAPI
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
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfiles(
                    typeof(ProductService),
                    typeof(ProductsController));
            });

            services.AddSingleton<IConnectionManager>(
                new ConnectionManager(Configuration.GetConnectionString("Products")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOptionRepository, ProductOptionRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductOptionService, ProductOptionService>();

            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = ApiVersion.Parse(Configuration["Versioning:Default"]);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ApiVersionReader = new QueryStringApiVersionReader(Configuration["Versioning:ParameterName"]);
            });

            services.AddMvc(cfg =>
            {
                cfg.Filters.Add<ExceptionResultFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}

