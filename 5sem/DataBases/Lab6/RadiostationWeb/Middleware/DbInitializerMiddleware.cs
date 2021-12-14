using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RadiostationWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public Task Invoke(HttpContext context, BDLab1Context dbContext)
        {
            if (!(context.Session.Keys.Contains("isDbInitialized")))
            {
                DbInitializer.InitializeDb(dbContext);
                context.Session.SetString("isDbInitialized", "Yes");
            }

            return _next.Invoke(context);
        }
    }
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}
