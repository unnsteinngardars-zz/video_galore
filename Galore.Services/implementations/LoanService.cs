using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Services.Interfaces;

namespace Galore.Services.implementations
{
    public class LoanService : ILoanService
    {
        public IEnumerable<TapeDTO> GetTapesOnLoanForUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        // user/userId/tapes/tapeId: Register new loan
        public int CreateLoan(LoanInputModel loan, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        // user/userId/tapes/tapeId: Return tape on loan
        public void ReturnTapeOnLoan(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        // user/userId/tapes/tapeId: Update loan information
        public void UpdateTapeOnLoan(LoanInputModel loan, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }
    }
}