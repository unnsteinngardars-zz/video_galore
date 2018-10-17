using System;
using System.ComponentModel.DataAnnotations;

namespace Galore.Models.Loan {
    public class Loan {
        // PK is TapeId and UserId together
        public int Id { get; set; }
        [Required]
        public int TapeId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
    }
}