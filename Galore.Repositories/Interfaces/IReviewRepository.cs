using System.Collections.Generic;
using Galore.Models.Review;

namespace Galore.Repositories.Interfaces
{
    public interface IReviewRepository
    {

        // users/userId/reviews : Get all reviews a user has made
        IEnumerable<Review> GetAllReviewsForUser(int userId);

        // users/userId/reviews/tapeId : Add new user review for tape
        int CreateUserReview(Review review, int userId, int tapeId);

        // tapes/tapeId/reviews/userId : Get user review for tape
        // users/userId/reviews/tapeId : Get user review for given tape
        Review GetUserReviewForTape(int userId, int tapeId);

        // users/userId/reviews/tapeId : Delete a user review
        // tapes/tapeId/reviews/userId : Delete a user review
        void DeleteUserReviewForTape(Review review);

        // users/userId/reviews/tapeId : Update user review
        // tapes/tapeId/reviews/userId : Update user review
        void UpdateUserReviewForTape(Review review, int userId, int tapeId);

        // tapes/reviews : Get reviews for ALL tapes
        IEnumerable<Review> GetAllReviewsForAllTapes();

        //tapes/tapeId/reviews : Get all reviews for tapeId
        IEnumerable<Review> GetAllReviewsForTape(int tapeId);

    }
}