using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDetails>> GetAllUsersAsync()
        {
            return await _context.Users.Include(r => r.Role).Include(s => s.StatusObj).ToListAsync();
        }

        public async Task<UserDetails> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(r => r.Role).Include(s => s.StatusObj).FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<UserDetails> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Include(r => r.Role).Include(s => s.StatusObj).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(UserDetails user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDetails user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
