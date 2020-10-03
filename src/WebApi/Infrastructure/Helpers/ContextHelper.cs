using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;


namespace NetCoreAxampleAuth.Infrastructure.Helpers
{
    public static class ContextHelper
    {
        public static string GetCorellationId(this HttpContext context)
        {
            return Activity.Current?.Id ?? context?.TraceIdentifier;
        }
    }
}
