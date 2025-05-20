using Chatter.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post);
    }

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p=>p.User).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
