using Microsoft.EntityFrameworkCore;
using TaskManager.Server.DTOs;
using TaskManager.Server.Models;

namespace TaskManager.Server.AppDb
{
    public class UserService
    {
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.User.FindAsync(id);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            if (!_context.User.Any(u => u.Id == user.Id)) return false;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> AuthenticateUserAsync(LoginDto login)
        {
            var user = await _context.User
                .FirstOrDefaultAsync( u => u.Login == login.LoginOrEmail || u.Email == login.LoginOrEmail);
            if (user == null) return null;
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return false;
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
