using System;
using System.ComponentModel.DataAnnotations;

namespace Galore.Models.User
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public bool Deleted { get; set; } = false;
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}