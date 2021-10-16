using Microsoft.AspNetCore.Builder;

namespace KenticoKontentBlog.Feature.Security
{
    public static class SecurityHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecureHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityHeaderMiddleware>();
        }
    }
}
