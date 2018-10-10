using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Recommendation;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;

namespace Galore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<User> GetReportByDate(string loanDate);

        IEnumerable<User> GetReportByDuration(string loanDuration);

        IEnumerable<User> GetReportByDurationAndDate(string loanDuration, string loanDate);
        int CreateUser(UserInputModel user);
        User GetUserById(int userId);
        void DeleteUser(User user);
        void UpdateUserById(UserInputModel user, int userId);

        // Tapes
        IEnumerable<Tape> GetTapesOnLoan(int userId);
        int RegisterTapeOnLoan(LoanInputModel loan, int userId, int tapeId);
        void ReturnTapeLoan(int userId, int tapeId);
        void UpdateTapeLoan(LoanInputModel loan, int userId, int tapeId);

        // Review
        IEnumerable<Review> GetAllReviews(int userId);
        Review GetUserReviewForTape(int userId, int tapeId);
        int CreateUserReviewForTape(ReviewInputModel review, int userId, int tapeId);
        void DeleteUserReviewForTape(Review review);

        // Recommendation
        Recommendation GetRecommendation(int userId);
    }
}