﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService3
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ViewResult)
            {
                var referrer_policy = "no-referrer";
                var csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";

                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options")) { context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff"); }
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options")) { context.HttpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN"); }
                if (!context.HttpContext.Response.Headers.ContainsKey("Content-Security-Policy")) { context.HttpContext.Response.Headers.Add("Content-Security-Policy", csp); }
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Security-Policy")) { context.HttpContext.Response.Headers.Add("X-Content-Security-Policy", csp); }
                if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy")) { context.HttpContext.Response.Headers.Add("Referrer-Policy", referrer_policy); }
            }
        }
    }
}
