using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<Tape> GetTapesOnLoanForUser(int userId);
        void RegisterTapeOnLoan(int userId, int tapeId, int id);
        void ReturnTapeOnLoan(Loan loan);
        void UpdateTapeOnLoan(Loan loan);
        IEnumerable<Loan> GetAllLoans();
    }
}