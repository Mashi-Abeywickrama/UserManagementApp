using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementApp.Models
{
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        [Required]
        public string DateOfBirth { get; set; }

        [ForeignKey("Role")]
        public int RoleType { get; set; }

        [ForeignKey("StatusObj")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public virtual RoleType Role { get; set; }
        public virtual Status StatusObj { get; set; }
    }
}
