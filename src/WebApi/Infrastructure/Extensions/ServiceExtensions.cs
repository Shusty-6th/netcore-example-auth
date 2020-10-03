using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NetCoreAxampleAuth.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Net Core Example API - V1",
                        Version = "v1"
                    }
                 );

                foreach (var filePath in System.IO.Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)), "*.xml"))
                {
                    try
                    {
                        c.IncludeXmlComments(filePath);
                    }
                    catch (Exception)
                    {
                        // blackhole, no included bad xml to swagger is ok
                    }
                }

                // optional for manually including xml files TODO: delete unnecessary lines on production
                // var filePath = Path.Combine(System.AppContext.BaseDirectory, "WebApi.xml");
                // c.IncludeXmlComments(filePath);
                // instead of xml you can use Swashbuckle adnotations
                // c.EnableAnnotations();
            });
        }
    }
}
