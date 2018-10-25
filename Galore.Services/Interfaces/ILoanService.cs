using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    //Interface for the loan service class
    public interface ILoanService
    {
        IEnumerable<TapeDTO> GetTapesOnLoanForUser(int userId);
        void RegisterTapeOnLoan(int userId, int tapeId);
        void ReturnTapeOnLoan(int userId, int tapeId);
        void UpdateTapeOnLoan(LoanInputModel loan, int userId, int tapeId);
        IEnumerable<Loan> GetLoans();
    }
}