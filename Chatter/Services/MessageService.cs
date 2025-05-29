using Chatter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(Message message);
        Task<List<Message>> GetConversationAsync(string user1Id, string user2Id);
    }

    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessageAsync(Message message)
        {
            message.Date = DateTime.UtcNow;
           _context.Messages.Add(message);
           await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetConversationAsync(string user1Id, string user2Id)
        {
            return await _context.Messages.Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                (m.SenderId == user2Id && m.ReceiverId == user1Id)).OrderBy(m => m.Date).ToListAsync();

        }
    }
}
