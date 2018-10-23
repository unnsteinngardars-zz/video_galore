using System.Collections.Generic;
using Galore.Models.Review;

namespace Galore.Services.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId);

        int CreateUserReview(ReviewInputModel review, int userId, int tapeId);

        ReviewDTO GetUserReviewForTape(int userId, int tapeId);

        void DeleteUserReviewForTape(int userId, int tapeId);

        void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId);

        IEnumerable<ReviewDTO> GetAllReviewsForAllTapes();

        IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId);

    }
}