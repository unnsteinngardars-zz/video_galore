using System.ComponentModel.DataAnnotations;
using TecRad.Models;

namespace Galore.Models.Loan
{
    public class LoanInputModel
    {
        [Required]
        [RegularExpression("[12][0-9]{3}-([0][123456789]|[1][012])-([012][123456789]|10|20|3[01])")]
        public string BorrowDate { get; set; }
    }
}