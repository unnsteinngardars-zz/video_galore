using TecRad.Models;

namespace Galore.Models.Loan {
    public class LoanDTO : HyperMediaModel {
        public int TapeId { get; set; }
        public int UserId { get; set; }
    }
}