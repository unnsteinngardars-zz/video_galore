using System.ComponentModel.DataAnnotations;
using TecRad.Models;

namespace Galore.Models.User
{
    public class UserInputModel : HyperMediaModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}