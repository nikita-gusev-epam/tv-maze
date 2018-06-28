using Microsoft.AspNetCore.Builder;

namespace EPAM.TvMaze.RestApi.Infrastructure.Correlation
{
    public static class CorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
