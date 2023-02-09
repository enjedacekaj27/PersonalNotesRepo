using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
