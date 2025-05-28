using Chatter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Services
{
    public interface ILikeService
    {
        Task AddLikeAsync(int postId, string userId);
        Task RemoveLikeAsync(int postId, string userId);
        Task<bool> IsLikedAsync(int postId, string userId);
        Task<int> GetLikesCountAsync(int postId);

    }
    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext _context;

        public LikeService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddLikeAsync(int postId, string userId)
        {
            var like = new Like
            {
                PostId = postId,
                UserId = userId,
                Date = DateTime.Now
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLikeAsync(int postId, string userId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

            if(like != null)
            {
                _context.Likes.Remove(like);
                _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsLikedAsync(int postId, string userId)
        {
            return await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _context.Likes.CountAsync(l => l.PostId == postId);
        }
    }
}
