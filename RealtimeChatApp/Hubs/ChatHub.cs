using Microsoft.AspNetCore.SignalR;
using RealtimeChatApp.Services;

namespace RealtimeChatApp.Hubs
{
    public class ChatHub : Hub
    {

        private readonly SentimentAnalysisService _sentimentService;

        public ChatHub(SentimentAnalysisService sentimentService)
        {
            _sentimentService = sentimentService;
        }

        public async Task SendMessage(string user, string message)
        {
            var sentiment = await _sentimentService.AnalyzeSentimentAsync(message);

            await Clients.All.SendAsync("ReceiveMessage", user, message, sentiment);
        }
    }
}
