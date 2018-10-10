using TecRad.Models;

namespace Galore.Models.Loan {
    public class LoanDTO : HyperMediaModel {
        public int Id { get; set; }
        public int TapeId { get; set; }
        public int UserId { get; set; }
    }
}