using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.SignalR;
using RealtimeChatApp.Models;
using RealtimeChatApp.Repository;
using RealtimeChatApp.Services;

namespace RealtimeChatApp.Hubs
{
    public class ChatHub : Hub
    {

        private readonly ILogger<ChatHub> _logger;
        private readonly ISentimentAnalysisService _sentimentService;
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatHub(ISentimentAnalysisService sentimentService, IChatMessageRepository chatMessageRepository, ILogger<ChatHub> logger)
        {
            _sentimentService = sentimentService;
            _chatMessageRepository = chatMessageRepository;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            DocumentSentiment sentiment;
            string sentimentStr = string.Empty;

            try
            {
                sentiment = await _sentimentService.AnalyzeSentimentAsync(message);
                sentimentStr = sentiment.Sentiment.ToString();
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Sentiment analysis failed for message '{Message}'.", message);
            }

            try
            {
                _chatMessageRepository.Create(new ChatMessage
                {
                    User = user,
                    Message = message,
                    Sentiment = sentimentStr
                });
                await _chatMessageRepository.SaveChangesAsync();

            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while saving the chat message for user {User} with message '{Message}' and sentiment {Sentiment}.", user, message, sentimentStr);
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message, sentimentStr);
        }
    }
}
