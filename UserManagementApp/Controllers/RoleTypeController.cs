using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementApp.Data;
using UserManagementApp.Models;

namespace UserManagementApp.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize]
    public class RoleTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleTypeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleType role)
        {
            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new ArgumentException("Role name cannot be empty.");

            _context.RoleTypes.Add(role);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleID }, role);
        }

        [HttpGet]
        public async Task<IEnumerable<RoleType>> GetRoles()
        {
            return await _context.RoleTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _context.RoleTypes.FindAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleType role)
        {
            var existingRole = await _context.RoleTypes.FindAsync(id);
            if (existingRole == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new ArgumentException("Role name cannot be empty.");

            existingRole.RoleName = role.RoleName;
            existingRole.ModifiedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            var successResponse = new
            {
                Message = "Role updated successfully.",
                RoleId = existingRole.RoleID,
                RoleName = existingRole.RoleName
            };

            return Ok(successResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.RoleTypes.FindAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {id} not found.");

            _context.RoleTypes.Remove(role);
            await _context.SaveChangesAsync();

            var successResponse = new
            {
                Message = "Role deleted successfully.",
                RoleId = role.RoleID,
                RoleName = role.RoleName
            };
            return Ok(successResponse);
        }
    }
}
