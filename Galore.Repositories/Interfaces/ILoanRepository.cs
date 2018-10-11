namespace Galore.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        IEnumerable<Tape> GetTapesOnLoanForUser(int userId);
        int RegisterTapeOnLoan(Loan loan, int userId, int tapeId);
        void ReturnTapeLoan(int userId, int tapeId);
        void UpdateTapeLoan(Loan loan, int userId, int tapeId);
    }
}