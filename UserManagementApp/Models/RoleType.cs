using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class RoleType
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(100)]
        public string RoleName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
