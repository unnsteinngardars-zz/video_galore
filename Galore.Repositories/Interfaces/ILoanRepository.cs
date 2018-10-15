using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<Tape> GetTapesOnLoanForUser(int userId);
        void RegisterTapeOnLoan(int userId, int tapeId);
        void ReturnTapeOnLoan(int userId, int tapeId);
        void UpdateTapeOnLoan(Loan loan, int userId, int tapeId);
        IEnumerable<Loan> GetAllLoans();
    }
}