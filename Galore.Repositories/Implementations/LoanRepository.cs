using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        public IEnumerable<Tape> GetTapesOnLoanForUser(int userId) 
        {
            throw new System.NotImplementedException();

        }
        public int RegisterTapeOnLoan(Loan loan, int userId, int tapeId) 
        {
            throw new System.NotImplementedException();

        }
        public void ReturnTapeLoan(int userId, int tapeId) 
        {
            throw new System.NotImplementedException();

        }
        public void UpdateTapeLoan(Loan loan, int userId, int tapeId) 
        {
            throw new System.NotImplementedException();

        }
    }
}