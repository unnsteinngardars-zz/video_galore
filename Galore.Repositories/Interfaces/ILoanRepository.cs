using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    //Interface for the loan repository
    public interface ILoanRepository
    {
        IEnumerable<Tape> GetTapesOnLoanForUser(int userId);
        void RegisterTapeOnLoan(int userId, int tapeId);
        void ReturnTapeOnLoan(Loan loan);
        void UpdateTapeOnLoan(Loan loan);
        IEnumerable<Loan> GetAllLoans();
    }
}