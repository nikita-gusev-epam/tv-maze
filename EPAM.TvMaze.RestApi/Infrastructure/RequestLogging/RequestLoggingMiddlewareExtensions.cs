using Microsoft.AspNetCore.Builder;

namespace EPAM.TvMaze.RestApi.Infrastructure.RequestLogging
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}