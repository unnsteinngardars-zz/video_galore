using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class TapeRepository : ITapeRepository
    {
        public int CreateTape(TapeInputModel tape)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTape(Tape tape)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUserReviewForTape(Review review)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Review> GetAllTapeReviews()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tape> GetAllTapes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tape> GetReportByDate(string loanDate)
        {
            throw new System.NotImplementedException();
        }

        public Tape GetTapeById(int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public Review GetTapeReviewById(int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public Review GetUserReviewForTape(int tapeId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTapeById(TapeInputModel tape, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserReviewForTape(ReviewInputModel review, int tapeId, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}