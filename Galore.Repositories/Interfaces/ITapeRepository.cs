using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    public interface ITapeRepository
    {
        IEnumerable<Tape> GetAllTapes();
        IEnumerable<Tape> GetReportByDate(string loanDate);
        int CreateTape(TapeInputModel tape);
        Tape GetTapeById(int tapeId);
        void DeleteTape(Tape tape);
        void UpdateTapeById(TapeInputModel tape, int tapeId);

        // Reviews 
        IEnumerable<Review> GetAllTapeReviews();
        Review GetTapeReviewById(int tapeId);
        Review GetUserReviewForTape(int tapeId, int userId);
        void UpdateUserReviewForTape(ReviewInputModel review, int tapeId, int userId);
        void DeleteUserReviewForTape(Review review);
    }
}