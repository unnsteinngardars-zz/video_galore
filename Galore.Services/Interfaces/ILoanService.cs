using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    public interface ILoanService
    {
        // user/userId/tapes: get tapes that user has on loan( user id, tape id)
        IEnumerable<TapeDTO> GetTapesOnLoanForUser(int userId);

        // user/userId/tapes/tapeId: Register new loan
        void RegisterTapeOnLoan(int userId, int tapeId);

        // user/userId/tapes/tapeId: Return tape on loan
        void ReturnTapeOnLoan(int userId, int tapeId);

        // user/userId/tapes/tapeId: Update loan information
        void UpdateTapeOnLoan(LoanInputModel loan, int userId, int tapeId);
    }
}