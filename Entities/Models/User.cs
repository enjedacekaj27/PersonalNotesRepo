
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models

{
    [Table("User")]
    public class User
    {
        [Key]

        public int ID { get; set; }

        [Required(ErrorMessage = "FirstName is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for FirstName is 60 characters.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for FirstName is 60 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(256, ErrorMessage = "Maximum length for Email is 256 characters.")]
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [MaxLength(256, ErrorMessage = "Maximum length for Password is 256 characters.")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; } 
    }
}
