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
    [Route("api/status")]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatusController(AppDbContext context)
        {
            _context = context;
        }

        //  Create a new status
        [HttpPost]
        public async Task<IActionResult> CreateStatus(Status status)
        {
            if (string.IsNullOrWhiteSpace(status.StatusName))
                throw new ArgumentException("Status name cannot be empty.");

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStatusById), new { id = status.StatusID }, status);
        }

        //  Get all statuses
        [HttpGet]
        public async Task<IEnumerable<Status>> GetStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        //  Get status by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                throw new KeyNotFoundException($"Status with ID {id} not found.");
            return Ok(status);
        }

        //  Update an existing status
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, Status status)
        {
            var existingStatus = await _context.Statuses.FindAsync(id);
            if (existingStatus == null)
                throw new KeyNotFoundException($"Status with ID {id} not found.");
            if (string.IsNullOrWhiteSpace(status.StatusName))
                throw new ArgumentException("Status name cannot be empty.");

            existingStatus.StatusName = status.StatusName;
            existingStatus.ModifiedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var successResponse = new
            {
                Message = "Status updated successfully.",
                StatusID = existingStatus.StatusID,
                StatusName = existingStatus.StatusName
            };
            return Ok(successResponse);
        }

        //  Delete a status
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                throw new KeyNotFoundException($"Status with ID {id} not found.");

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            var successResponse = new
            {
                Message = "Status deleted successfully.",
                StatusID = status.StatusID,
                StatusName = status.StatusName
            };
            return Ok(successResponse);
        }
    }
}
