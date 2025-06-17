using PayTrack.Application;
using System.Net;
using System.Text.Json;

namespace PayTrack.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"Custom exception: {ex.Message}");
                await HandleCustomExceptionAsync(httpContext, ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorId = Guid.NewGuid();

            _logger.LogError(exception,
                "[{ErrorId}] Exception: \nPath: {Path}\nMessage: {Message}",
                errorId, context.Request.Path, exception.Message);

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                ErrorId = errorId
            });

            return context.Response.WriteAsync(result);
        }
        private Task HandleCustomExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            _logger.LogError(exception,
                "{Message}", exception.Message);

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
            });

            return context.Response.WriteAsync(result);
        }
    }
}
