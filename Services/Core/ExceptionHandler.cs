using System.Diagnostics;

using Microsoft.AspNetCore.Diagnostics;

namespace Happy_Devs_BE.Services.Core
{
    public class TraceExceptionLogger : IExceptionHandler
    {
        private readonly ILogger<TraceExceptionLogger> _logger;
        public TraceExceptionLogger(ILogger<TraceExceptionLogger> logger)
        {
            _logger = logger;
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string message = exception.Message;
            _logger.LogError($"error has occured at {DateTime.UtcNow}: {message}");

            return ValueTask.FromResult(false);
        }
    }
}
