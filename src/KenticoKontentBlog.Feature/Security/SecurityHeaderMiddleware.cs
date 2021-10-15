using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace KenticoKontentBlog.Feature.Security
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Remove headers which expose the technology stack.
            context.Response.Headers.Remove("x-powered-by");
            context.Response.Headers.Remove("server");

            // Add headers to instruct browser behaviour
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");

            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; script-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; img-src 'self' data: https:; frame-src 'self' https://www.youtube-nocookie.com/;");

            await _next.Invoke(context);
        }
    }
}
