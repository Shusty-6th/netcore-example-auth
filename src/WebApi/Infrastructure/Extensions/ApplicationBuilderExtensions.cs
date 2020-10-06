using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Elasticsearch.Providers;
using Audit.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace NetCoreExampleAuth.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void RegisterAuditNet(this IApplicationBuilder app, IConfiguration conf)
        {
            Audit.Core.Configuration.DataProvider = new ElasticsearchDataProvider()
            {
                ConnectionSettings = new AuditConnectionSettings(new Uri(conf["ElasticSettings:Url"]))
                                        .BasicAuthentication(conf["ElasticSettings:User"]
                                            , conf["ElasticSettings:Password"]),
                IndexBuilder = ev => conf["ElasticSettings:Index"],//+ev.EventType,
                IdBuilder = ev => Guid.NewGuid()
            };

            app.UseAuditMiddleware(_ => _
                    .FilterByRequest(rq => !rq.Path.Value.EndsWith("favicon.ico"))
                    .WithEventType("{verb}:{url}")
//                    .IncludeHeaders()
//                    .IncludeResponseHeaders()
                    .IncludeResponseBody());
                    //.IncludeResponseBody(ctx => ctx.Response.StatusCode != StatusCodes.Status200OK));
        }

        public static void UseSwaggerWithUi(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // swagger available at ttp://localhost:<port>/ instead of ttp://localhost:<port>/swagger
            });
        }
    }
}
