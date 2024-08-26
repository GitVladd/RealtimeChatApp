using RealtimeChatApp.Data;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        ApplicationDbContext _context;

        public ChatMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(ChatMessage entity)
        {
            _context.ChatMessages.Add(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
