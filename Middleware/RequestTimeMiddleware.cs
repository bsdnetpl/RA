using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RA.Middleware
{
    public class RequestTimeMiddleware:IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopWatch;

        public RequestTimeMiddleware( ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
        }
      public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();
            var ElapsedMilliseconds = _stopWatch.ElapsedMilliseconds;
            if(ElapsedMilliseconds / 1000 >4)
            {
                var message = $"Request[{context.Request.Method}] at {context.Request.Path} took {ElapsedMilliseconds} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
