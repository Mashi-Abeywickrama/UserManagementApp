using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Models;
using UserManagementApp.Repositories;
using BCrypt.Net;

namespace UserManagementApp.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDetails user)
        {
            if (await _userRepository.GetUserByEmailAsync(user.Email) != null)
                throw new ArgumentException("Email already exists");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDetails user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException($"USer with ID {id} not found.");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.RoleType = user.RoleType;
            existingUser.Status = user.Status;
            existingUser.ModifiedAt = System.DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(existingUser);
            return Ok(new { Message = "User updated successfully.", UserId = id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            await _userRepository.DeleteUserAsync(id);
            var successResponse = new
            {
                Message = "User deleted successfully.",
                UserId = id,
                Email = user.Email
            };

            return Ok(successResponse);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException($"User with Email {email} not found.");
            return Ok(user);
        }

    }
}
