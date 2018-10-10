using TecRad.Models;

namespace Galore.Models.Loan
{
    public class LoanInputModel : HyperMediaModel
    {
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        
    }
}