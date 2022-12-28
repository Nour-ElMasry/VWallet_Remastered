using System.Net;

namespace Application.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomMiddleware(RequestDelegate next, ILoggerFactory logFactory)
    {
        _next = next;
        _logger = logFactory.CreateLogger<CustomMiddleware>();
    }

    public async Task Invoke(HttpContext httpContext)
    {
        _logger.LogInformation($"HTTP request is ---> Method: {httpContext.Request.Method} --> Path: {httpContext.Request.Path}");
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private Task HandleException(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

        if (ex is NullReferenceException || ex is ArgumentException) code = HttpStatusCode.BadRequest;

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;

        return Task.Run(() => _logger.LogError($"HTTP response is ---> Code: {httpContext.Response.StatusCode} --> Message: {ex.Message}"));
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}
