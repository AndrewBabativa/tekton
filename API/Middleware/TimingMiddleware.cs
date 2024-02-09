using System.Diagnostics;
using System.Text;

public class TimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimingMiddleware> _logger;

    public TimingMiddleware(RequestDelegate next, ILogger<TimingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context);

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        LogResponseTime(context.Request.Method, context.Request.Path, elapsedMilliseconds);
    }

    private void LogResponseTime(string method, string path, long elapsedMilliseconds)
    {
        var logMessage = $"{DateTime.Now} - {method} {path} took {elapsedMilliseconds}ms";
        var logFilePath = "response_times.log";

        try
        {
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error writing to log file: {ex.Message}");
        }
    }
}
