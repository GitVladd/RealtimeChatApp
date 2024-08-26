using Azure;
using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SentimentAnalysisService> _logger;
        private readonly TextAnalyticsClient _client;

        public SentimentAnalysisService(IConfiguration configuration, ILogger<SentimentAnalysisService> logger)
        {
            _logger = logger;
            _configuration = configuration;

            string endpoint = configuration["AzureSentimentAnalysisService:Endpoint"];
            string apiKey = configuration["AzureSentimentAnalysisService:Key"];
            var credentials = new AzureKeyCredential(apiKey);
            _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        public async Task<DocumentSentiment> AnalyzeSentimentAsync(string message)
        {
            try
            {
                return await _client.AnalyzeSentimentAsync(message);
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "An error occurred while analyzing sentiment for message '{Message}'.", message);
                throw;
            }
        }
    }
}
