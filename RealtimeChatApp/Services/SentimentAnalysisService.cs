using Azure;
using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Services
{
    public class SentimentAnalysisService
    {
        private readonly TextAnalyticsClient _client;

        public SentimentAnalysisService(string endpoint, string apiKey)
        {
            var credentials = new AzureKeyCredential(apiKey);
            _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        public async Task<string> AnalyzeSentimentAsync(string message)
        {
            DocumentSentiment sentiment = await _client.AnalyzeSentimentAsync(message);
            return sentiment.Sentiment.ToString();
        }
    }
}
