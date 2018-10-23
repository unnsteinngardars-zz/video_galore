using System;
using TecRad.Models;

namespace Galore.Models.Loan
{
    public class LoanDetailDTO : HyperMediaModel
    {
        public int TapeId { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}