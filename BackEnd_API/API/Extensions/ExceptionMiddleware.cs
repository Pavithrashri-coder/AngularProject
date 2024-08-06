using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text.Json;

namespace API
{
    public class ExceptionMiddleware
    {
        private const string CorrelationIdHeaderKey = "CorrelationId";
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private string? correlationId = null;
        public ExceptionMiddleware(RequestDelegate next,
        ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
                {
                    _logger.LogInformation($"CorrelationIdcount from Request Header:{correlationIds.Count}");
                    if (correlationIds.Count > 0)
                    {
                        correlationId = correlationIds[0];
                    }

                    _logger.LogInformation($"CorrelationId from Request Header:{correlationId}");
                }

                httpContext.Response.OnStarting(() =>
                {
                    if (!httpContext.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out correlationIds))
                        httpContext.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
                    return Task.CompletedTask;
                });


                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //throw;
                await HandleException(httpContext, ex);
            }

        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
            {
                correlationId = correlationIds[0];
            }
            else if (context.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues ids))
            {
                correlationId = ids[0];
            }
            //var errorMessage = JsonSerializer.Serialize(new { Message = $"Something went wrong. Please contact administrator with Reference Id: {correlationId}" });
            var errorMessage = JsonSerializer.Serialize(new
            {
                errorType = "TOKEN_ERROR",
                message = $"Something went wrong. Please contact administrator",
                correlationId,
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
