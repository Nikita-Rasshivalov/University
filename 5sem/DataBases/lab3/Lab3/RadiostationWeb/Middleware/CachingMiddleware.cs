using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using RadiostationWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Middleware
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;

        public CachingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IMemoryCache cache, BDLab1Context dbContext)
        {
            var records = dbContext.RecordsViews.Take(20).ToList();
            if (records != null)
            {
                cache.Set("records", records,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 7 + 240)));
            }
            var performers = dbContext.PerformersViews.Take(20).ToList();
            if (performers != null)
            {
                cache.Set("performers", performers,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 7 + 240)));
            }
            var employees = dbContext.Employees.Take(20).ToList();
            if (employees != null)
            {
                cache.Set("employees", employees,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 7 + 240)));
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCachingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CachingMiddleware>();
        }
    }
}
