namespace TaskManagementSystem.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private static readonly string LogFilePath = "logs/requests.log";

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;

        var logEntry = $"{DateTime.UtcNow:O} {method} {path}";

        Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath)!);
        await File.AppendAllTextAsync(LogFilePath, logEntry + Environment.NewLine);

        _logger.LogInformation(logEntry);

        await _next(context);
    }
}
