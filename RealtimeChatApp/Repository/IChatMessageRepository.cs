using RealtimeChatApp.Models;

namespace RealtimeChatApp.Repository
{
    public interface IChatMessageRepository
    {
        void Create(ChatMessage entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
