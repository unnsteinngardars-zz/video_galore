using System;
using TecRad.Models;

namespace Galore.Models.Loan {
    public class LoanDetailsDTO : HyperMediaModel {
        public int Id { get; set; }
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}