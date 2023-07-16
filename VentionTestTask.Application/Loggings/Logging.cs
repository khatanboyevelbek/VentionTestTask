using Microsoft.Extensions.Logging;

namespace VentionTestTask.Application.Loggings
{
    public class Logging : ILogging
    {
        private readonly ILogger<Logging> logger;

        public Logging(ILogger<Logging> logger) =>
            this.logger = logger;

        public void LogCritical(Exception exception)
        {
            this.logger.LogCritical(exception, message: exception.Message);
        }

        public void LogError(Exception exception)
        {
            this.logger.LogError(exception, message: exception.Message);
        }

        public void LogInformation(string message)
        {
            this.logger.LogInformation(message);
        }
    }
}
