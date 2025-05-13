using System.Data;

namespace Chatter.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public  string? ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
