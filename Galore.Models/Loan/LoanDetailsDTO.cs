using TecRad.Models;

namespace Galore.Models.Loan {
    public class LoanDetailsDTO : HyperMediaModel {
        public int Id { get; set; }
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }

    }
}