using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Models;

namespace UserManagementApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDetails>> GetAllUsersAsync();
        Task<UserDetails> GetUserByIdAsync(int id);
        Task<UserDetails> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserDetails user);
        Task UpdateUserAsync(UserDetails user);
        Task DeleteUserAsync(int id);
    }
}
