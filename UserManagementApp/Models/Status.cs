using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required, MaxLength(100)]
        public string StatusName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
