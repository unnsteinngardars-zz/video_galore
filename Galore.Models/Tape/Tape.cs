using System;
using System.ComponentModel.DataAnnotations;

namespace Galore.Models.Tape {
    public class Tape {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string DirectorFirstName { get; set; }
        [Required]
        public string DirectorLastName { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string EIDR { get; set; }
        [Required]
        public bool Deleted { get; set; } = false;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required]
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}