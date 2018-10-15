using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        public IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public int CreateUserReview(ReviewInputModel review, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public ReviewDTO GetUserReviewForTape(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }
        
        public void DeleteUserReviewForTape(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForAllTapes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId)
        {
            throw new System.NotImplementedException();
        }
    }
}