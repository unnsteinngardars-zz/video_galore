using System;

namespace Galore.Models.Loan {
    public class Loan {
        public int Id { get; set; }
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}