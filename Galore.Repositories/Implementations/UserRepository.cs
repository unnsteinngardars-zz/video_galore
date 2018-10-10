using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Recommendation;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public int CreateUser(UserInputModel user)
        {
            throw new System.NotImplementedException();
        }

        public int CreateUserReviewForTape(ReviewInputModel review, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserReviewForTape(Review review)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Review> GetAllReviews(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public Recommendation GetRecommendation(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetReportByDate(string loanDate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetReportByDuration(string loanDuration)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetReportByDurationAndDate(string loanDuration, string loanDate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tape> GetTapesOnLoan(int userId)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Review GetUserReviewForTape(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public int RegisterTapeOnLoan(LoanInputModel loan, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void ReturnTapeLoan(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTapeLoan(LoanInputModel loan, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserById(UserInputModel user, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}