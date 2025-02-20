using System.Threading.Tasks;
using UserManagementApp.Models;

namespace UserManagementApp.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
    }
}
