using System;

namespace Galore.Models.Loan {
    public class Loan {
        // PK is TapeId and UserId together
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}