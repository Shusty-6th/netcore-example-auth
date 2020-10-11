using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;


namespace NetCoreExampleAuth.Infrastructure.Helpers
{
    public static class ContextHelper
    {
        public static string GetCorellationId(this HttpContext context)
        {
            //return Activity.Current?.Id ?? context?.TraceIdentifier;
            return context?.TraceIdentifier ?? Activity.Current?.Id ?? Guid.NewGuid().ToString();
        }
    }
}
