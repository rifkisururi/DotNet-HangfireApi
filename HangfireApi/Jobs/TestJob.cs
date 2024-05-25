namespace HangfireApi.Jobs
{
    public class TestJob
    {
        private readonly ILogger _logger;
        public TestJob(ILogger<TestJob> logger) { 
            _logger = logger;
        }

        public void WriteLog(string LogMessage) {
            _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss tt} {LogMessage}");
        }
    }
}
