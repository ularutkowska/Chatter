using Chatter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task CreateCommentAsync(Comment comment);

    }
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        
        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments.Where(c=>c.Id == postId).Include(c => c.User).OrderBy(c => c.Date).ToListAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id ==  commentId);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
