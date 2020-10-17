using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Audit.Elasticsearch.Providers;
using Audit.WebApi;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreExampleAuth.BusinessLogic;
using NetCoreExampleAuth.Domain.Core;
using NetCoreExampleAuth.Domain.Core.Repositories;
using NetCoreExampleAuth.Domain.Persistence;
using NetCoreExampleAuth.Domain.Persistence.Repositories;
using NetCoreExampleAuth.Infrastructure.Extensions;
using NetCoreExampleAuth.Infrastructure.Hubs;
using NetCoreExampleAuth.Patterns.Configs;

namespace NetCoreExampleAuth
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
            services.ConfigureCors();


            services.ConfigureSqlContext(Configuration);
            services.AddAuthentication();
            services.ConfigureIdentity();

            // Authentication configuration (JWT)
            services.ConfigureJWT(Configuration);
            
            services.AddSignalR();

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.ConfigureSwaggerDoc();


            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<ElasticSettings>(Configuration.GetSection("ElasticSettings"));


            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

//            app.RegisterAuditNet(Configuration);

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            //app.UseStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            // use authentication 
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });

            // swagger
            app.UseSwaggerWithUi();
        }
    }
}
