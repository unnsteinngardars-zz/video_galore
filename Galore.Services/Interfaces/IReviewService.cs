using System.Collections.Generic;
using Galore.Models.Review;

namespace Galore.Services.Interfaces
{
    public interface IReviewService
    {
        // users/userId/reviews : Get all reviews a user has made
        IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId);

        // users/userId/reviews/tapeId : Add new user review for tape
        int CreateUserReview(ReviewInputModel review, int userId, int tapeId);

        // tapes/tapeId/reviews/userId : Get user review for tape
        // users/userId/reviews/tapeId : Get user review for given tape
        ReviewDetailsDTO GetUserReviewForTape(int userId, int tapeId);
        
        // users/userId/reviews/tapeId : Delete a user review
        // tapes/tapeId/reviews/userId : Delete a user review
        void DeleteUserReviewForTape(int userId, int tapeId);

        // users/userId/reviews/tapeId : Update user review
        // tapes/tapeId/reviews/userId : Update user review
        void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId);

        // tapes/reviews : Get reviews for ALL tapes
        IEnumerable<ReviewDTO> GetAllReviewsForAllTapes(); 

        //tapes/tapeId/reviews : Get all reviews for tapeId
        IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId);
         
    }
}