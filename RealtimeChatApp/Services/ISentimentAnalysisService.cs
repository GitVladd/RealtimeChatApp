using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Services
{
    public interface ISentimentAnalysisService
    {
        Task<DocumentSentiment> AnalyzeSentimentAsync(string message);
    }
}
