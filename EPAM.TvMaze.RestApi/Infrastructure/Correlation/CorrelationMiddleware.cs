using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EPAM.TvMaze.RestApi.Infrastructure.Correlation
{
    public class CorrelationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            await next(context);
        }
    }
}
