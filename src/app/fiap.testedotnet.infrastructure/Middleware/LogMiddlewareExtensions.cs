using Microsoft.AspNetCore.Builder;

namespace fiap.testedotnet.infrastructure.Middleware
{
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(LogMiddleware));
        }
    }
}
