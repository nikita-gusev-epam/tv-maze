using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace EPAM.TvMaze.RestApi.Infrastructure.RequestLogging
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private static readonly ILogger Logger = Log.ForContext<RequestLoggingMiddleware>();

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Logger.Information("Started processing request {From} to {To}", context.Connection.RemoteIpAddress.ToString(), context.Request.GetDisplayUrl());
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Failed to process request");
                throw;
            }
            finally
            {
                Logger.Information("Finished processing request. Response code: {StatusCode}", context.Response.StatusCode);
            }
        }
    }
}
