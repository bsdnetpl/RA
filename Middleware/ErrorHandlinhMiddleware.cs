using Microsoft.AspNetCore.Html;
using RA.services;
using System;
using System.Threading.Tasks;

namespace RA.Middleware
{
    public class ErrorHandlinhMiddleware : IMiddleware
    {
        private readonly ILogger <ErrorHandlinhMiddleware>_logger;
        public ErrorHandlinhMiddleware(ILogger<ErrorHandlinhMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);

            }
            catch (NotFundExcept notfundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfundException.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e,e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Somthing went wrong");
            }
        }
    }
}
