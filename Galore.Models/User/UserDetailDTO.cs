using System.Collections.Generic;
using Galore.Models.Loan;

namespace Galore.Models.User
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<LoanDTO> BorrowHistory { get; set; }

    }
}