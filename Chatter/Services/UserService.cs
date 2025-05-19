using Chatter.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Chatter.Services
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<User> GetUserByIdAsync(string userId);
        Task<SignInResult> SignInUserAsync(string username, string password);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<SignInResult> SignInUserAsync(string username, string password)
        {
            var user = await _userManager.FindByIdAsync(username);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                return result;
            }

            return SignInResult.Failed;
        }
    }
}
