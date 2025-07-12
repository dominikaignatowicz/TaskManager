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
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            if (!_context.Users.Any(u => u.Id == user.Id)) return false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> AuthenticateUserAsync(LoginDto login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync( u => u.Login == login.LoginOrEmail || u.Email == login.LoginOrEmail);
            if (user == null) return null;
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            await _context.SaveChangesAsync();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
