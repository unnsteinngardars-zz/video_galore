using System.Collections.Generic;
using Galore.Models.Review;

namespace Galore.Repositories.Interfaces
{
    //Interface for the review repository
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviewsForUser(int userId);
        int CreateUserReview(Review review, int userId, int tapeId);
        Review GetUserReviewForTape(int userId, int tapeId);
        void DeleteUserReviewForTape(Review review);
        void UpdateUserReviewForTape(Review review, int userId, int tapeId);
        IEnumerable<Review> GetAllReviewsForAllTapes();
        IEnumerable<Review> GetAllReviewsForTape(int tapeId);
    }
}